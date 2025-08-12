<?php 
//VPS Connection
$hostname_ncon = "194.233.92.95";
$username_ncon = "admin";
$password_ncon = "FiH#4AVp4ze^xqcsslhtR"; // OLD PASSWORD - "FiHAVp4zexqclht0"; 
$mysqli_database = "report_db";

// //AWS Connection
// $hostname_ncon = "ledx-database-production-1.cdclzi8tmu83.ap-south-1.rds.amazonaws.com";
// $username_ncon = "ledxlntu_sync_live";
// $password_ncon = "EpSAO8mvKd}9";
// $mysqli_database = "ledxlntu_sync_live";

$destinationDB = mysqli_connect($hostname_ncon, $username_ncon, $password_ncon, $mysqli_database);

if (mysqli_connect_errno()) {

    echo "Failed to connect to MySQL: " . mysqli_connect_error();

    die;

}

function curl($url) {
    // Assigning cURL options to an array
    //curl_setopt($curl, CURLOPT_HEADER, true);
        $options = Array(
            CURLOPT_RETURNTRANSFER => TRUE,  // Setting cURL's option to return the webpage data
            CURLOPT_FOLLOWLOCATION => TRUE,  // Setting cURL to follow 'location' HTTP headers
            CURLOPT_AUTOREFERER => TRUE, // Automatically set the referer where following 'location' HTTP headers
            CURLOPT_CONNECTTIMEOUT => 120,   // Setting the amount of time (in seconds) before the request times out
            CURLOPT_TIMEOUT => 120,  // Setting the maximum amount of time for cURL to execute queries
            CURLOPT_MAXREDIRS => 10, // Setting the maximum number of redirections to follow
            CURLOPT_USERAGENT => "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1a2pre) Gecko/2008073000 Shredder/3.0a2pre ThunderBrowse/3.2.1.8",  // Setting the useragent
            CURLOPT_URL => $url, // Setting cURL's URL option with the $url variable passed into the function
            
        );
         
        $ch = curl_init();  // Initialising cURL 
        curl_setopt_array($ch, $options);   // Setting cURL's options using the previously assigned array data in $options
        $data = curl_exec($ch); // Executing the cURL request and assigning the returned data to the $data variable
        curl_close($ch);    // Closing cURL 
        return $data;   // Returning the data from the function 
 }

function query($sql)

{

    global $destinationDB;

    if (trim($sql != "")) {

        $query_id = mysqli_query($destinationDB, $sql);
        echo mysqli_error($destinationDB);
        return $query_id;

    } else

        return false;

}

function insert($table = null, $data)

{

    global $destinationDB;

    $query = "INSERT INTO `" . $table . "` ";

    $v     = '';

    $k     = '';

    foreach ($data as $key => $val) {

        //$val = escape($val); // filter input value

        $val = str_replace("'","",$val);

        $k .= '`'.$key.'`, ';

		$v .= "'".$val."',";

    }

    $query .= '(' . rtrim($k, ", ") . ') VALUES (' . rtrim($v, ", ") . ');';

    $res = query($query); // Run mysqli query

    if (!$res){

        echo $error =  mysqli_error($destinationDB);

		

		file_put_contents('testFile8.txt', date('Y-m-d H:i:s').($query.$error).PHP_EOL , FILE_APPEND | LOCK_EX);

	}

		

    return mysqli_insert_id($destinationDB);

	

}

function update($table = null, $data, $where = '1')

{

    global $destinationDB;

    if ($table === null or empty($data) or !is_array($data)) {

        echo "Invalid array for table: <b>" . $table . "</b>.";

        return false;

    }

    $q = 'UPDATE `' . $table . '` SET ';

    foreach ($data as $key => $val) {

        //$val = escape($val); // filter input value
        $val = str_replace("'","",$val);

        $q .= '`'.$key.'`="' . $val . '", ';

    }

   $q   = rtrim($q, ", ") . " WHERE " . $where . ";";

    $res = query($q); // Run mysqli query

    if (!$res)

        echo mysqli_error($destinationDB);

    return mysqli_affected_rows($destinationDB);

}


function fetchOneRow($sql)

{

    $record = query($sql);

    if (mysqli_num_rows($record) > 0)

        return mysqli_fetch_assoc($record);

    else

        return false;

}


function delete($table, $where = '')

{

    global $destinationDB;

    $query = !$where ? 'DELETE FROM ' . $table : 'DELETE FROM ' . $table . ' WHERE ' . $where;

    return query($query); // Run mysqli query

}

function multi_query($sql)

{

    global $destinationDB;

    if (trim($sql != "")) {

        $query_id = mysqli_multi_query($destinationDB, $sql);
        echo mysqli_error($destinationDB);
        return $query_id;

    } else

        return false;

}

/**

* Returns all the results

* @param mixed $sql

* @return mysqli result

*/

function fetchAll($sql)

{

    $record = query($sql);

    return $record;

}


?>


