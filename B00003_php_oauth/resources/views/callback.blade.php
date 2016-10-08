<?php
use Illuminate\Support\Facades\Request;
?>
@extends('master')

@section('content')
 
<div class="container">
	<div class="row">
		<div class="col-md-offset-2 col-md-8">
			<form role="form" action="/oauth/access_token" method="post">
				<div class="form-group">
					<label>grant_type</label>
					<input class="form-control" type="text" name="grant_type" value="authorization_code">
				</div>
				<div class="form-group">
					<label>client_id</label>
					<input class="form-control" type="text" name="client_id" value="crm2">
				</div>
				<div class="form-group">
					<label>client_secret</label>
					<input class="form-control" type="text" name="client_secret" value="iIR22w9bZmRlk7HwnOCT7k6GfmEutdoX">
				</div>
				<div class="form-group">
					<label>redirect_uri</label>
					<input class="form-control" type="text" name="redirect_uri" value="http://121.42.170.15:9494/callback">
				</div>
				<div class="form-group">
					<label>code</label>
					<input class="form-control" type="text" name="code" value="{{ Request::get('code') }}" readonly="readonly">
				</div>
				<div class="form-group">
					<label>state</label>
					<input class="form-control" type="text" name="state" value="{{ Request::get('state') }}" readonly="readonly">
				</div>
				<div class="form-group">
					<label>scope</label>
					<input class="form-control" type="text" name="scope" value="scope1">
				</div>
				<input
					type="hidden" name="_token" value="<?php echo csrf_token(); ?>"> 
				<button class="btn btn-primary" type="submit">授权</button>
			</form>
		</div>	
	</div>
</div>
@stop