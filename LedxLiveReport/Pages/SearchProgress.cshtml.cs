using ClosedXML.Excel;                                   // <- NEW
using LedxLiveReport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;                 // for QueryHelpers
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class SearchProgressModel : PageModel
{
    [BindProperty]
    public string SearchTerm { get; set; } = "";

    public List<StudentProgress> Results { get; set; } = new();
    public string ErrorMessage { get; set; } = "";

    private static readonly HttpClient _http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };

    private static readonly JsonSerializerOptions _jsonOpts = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public void OnGet() { }

    // --- existing search handler (keep yours as-is) ---
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            Results = new List<StudentProgress>();
            ErrorMessage = "";

            if (string.IsNullOrWhiteSpace(SearchTerm))
                return Page();

            var url = BuildApiUrl(SearchTerm, limit: 20);        // same rules as before
            using var resp = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode) { ErrorMessage = $"API HTTP {(int)resp.StatusCode}: {resp.ReasonPhrase}"; return Page(); }

            var parsed = JsonSerializer.Deserialize<StudentProgressResult>(body, _jsonOpts);
            if (parsed?.success != true) { ErrorMessage = parsed?.error ?? "API error"; return Page(); }

            Results = parsed.data ?? new List<StudentProgress>();
            return Page();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return Page();
        }
    }

    // --- NEW: Export to Excel handler ---
    public async Task<IActionResult> OnPostExportAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                // Friendly message if they click Export with no search
                ErrorMessage = "Please enter at least one ID or login before exporting.";
                return Page();
            }

            // Grab up to 1000 rows (the PHP endpoint caps at 1000)
            var url = BuildApiUrl(SearchTerm, limit: 1000, orderBy: "last_completed_date", order: "desc");

            using var resp = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var body = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode) return BadRequest($"API {(int)resp.StatusCode}: {resp.ReasonPhrase}");

            var parsed = JsonSerializer.Deserialize<StudentProgressResult>(body, _jsonOpts);
            var rows = parsed?.data ?? new List<StudentProgress>();

            // Build Excel
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("StudentProgress");

            // Header
            int r = 1;
            ws.Cell(r, 1).Value = "User ID";
            ws.Cell(r, 2).Value = "User Login";
            ws.Cell(r, 3).Value = "NicName";
            ws.Cell(r, 4).Value = "Name";
            ws.Cell(r, 5).Value = "Course";
            ws.Cell(r, 6).Value = "Total";
            ws.Cell(r, 7).Value = "Completed";
            ws.Cell(r, 8).Value = "%";
            ws.Cell(r, 9).Value = "Last Completed";

            var header = ws.Range(r, 1, r, 7);
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.FromHtml("#f3f4f6");
            header.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            // Data
            foreach (var item in rows)
            {
                
                r++;
                ws.Cell(r, 1).Value = item.user_id;
                ws.Cell(r, 2).Value = item.user_login;
                ws.Cell(r, 3).Value = item.Nicname;
                ws.Cell(r, 4).Value = item.Name;
                ws.Cell(r, 5).Value = item.course_name;
                ws.Cell(r, 6).Value = item.total_lessons;
                ws.Cell(r, 7).Value = item.completed_lessons;

                // Percent is a number like 52.27 in your API; keep as number with 2 decimals
                ws.Cell(r, 8).Value = item.completion_percent;
                ws.Cell(r, 8).Style.NumberFormat.Format = "0.00";

                // Try parse date to real Excel date/time
                if (DateTime.TryParse(item.last_completed_date, out var dt))
                {
                    ws.Cell(r, 9).Value = dt;
                    ws.Cell(r, 9).Style.DateFormat.Format = "yyyy-mm-dd hh:mm";
                }
                else
                {
                    ws.Cell(r, 9).Value = item.last_completed_date ?? "";
                }
            }

            // Autofit, freeze header, and add filter
            ws.Columns().AdjustToContents();
            ws.SheetView.FreezeRows(1);
            ws.Range(1, 1, r, 9).SetAutoFilter();

            // Stream to browser
            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"StudentProgress_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream.ToArray(), contentType, fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // --- helper to build the exact API URL you already use (CSV-aware, ids vs logins) ---
    private string BuildApiUrl(string term, int limit, string orderBy = "last_completed_date", string order = "desc")
    {
        var baseUrl = "https://ledx.law/wp-admin/get_student_progress.php";
        var token = "ApiConntoken";

        var query = new Dictionary<string, string?>
        {
            ["token"] = token,
            ["limit"] = limit.ToString(),
            ["order_by"] = orderBy,
            ["order"] = order
        };

        term = (term ?? "").Trim();

        if (term.Contains(","))
        {
            var parts = term.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => p.Trim())
                            .ToArray();

            bool allNumeric = parts.All(p => long.TryParse(p, out _));
            if (allNumeric)
                query["user_ids"] = string.Join(",", parts);
            else
                query["user_logins"] = string.Join(",", parts);
        }
        else
        {
            if (long.TryParse(term, out _))
                query["user_ids"] = term;
            else
                query["user_logins"] = term;
        }

        return QueryHelpers.AddQueryString(baseUrl, query!);
    }
}
