<?php
/**
 * 此實例是參考mantis soap實現;其他細節參考api
 * 代碼中踩到的各種坑見印象筆記：PHP  SOAP 中遇到的各种坑
 * 
 */
ini_set("soap.wsdl_cache_enabled", 0);
ini_set("soap.wsdl_cache_ttl", 0);
 $soap_client=new SoapClient('http://localhost/web_service/soap/mantisconnect.php?wsdl',['cache_wsdl'=>'WSDL_CACHE_NONE']);
 var_dump($soap_client->__getFunctions());
 $result1=$soap_client->__soapCall('add',[5,4]);
 echo "5+4=$result1";

 $result2=$soap_client->__soapCall('cut',[100,8]);
 echo "100-8=$result2";
 echo "current soap webserve verion:".$soap_client->get_version();