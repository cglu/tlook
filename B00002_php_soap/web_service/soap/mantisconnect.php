<?php
ini_set("soap.wsdl_cache_enabled", 0);
ini_set("soap.wsdl_cache_ttl", 0);
require_once 'core.php';
$wsdl="mantisconnect.wsdl";
$soap_server=new SoapServer($wsdl,['cache_wsdl'=>'WSDL_CACHE_NONE']);
$soap_server->addFunction(SOAP_FUNCTIONS_ALL);
$soap_server->handle();