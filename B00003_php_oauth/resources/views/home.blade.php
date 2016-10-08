@extends('master')

@section('content')
<div class="container">
	<div class="row">
		<div class="col-md-offset-2 col-md-8">
			<div class="well">
				<p>授权自定义</p>
			</div>
			<form role="form" action="/oauth/authorize" method="get">
				<div class="form-group">
					<label>client_id</label>
					<input class="form-control" type="text" name="client_id" placeholder="crm2">
				</div>
				<div class="form-group">
					<label>redirect_uri</label>
					<input class="form-control" type="text" name="redirect_uri" placeholder="http://121.42.170.15:9494/callback">
				</div>
				<div class="form-group">
					<label>scope</label>
					<input class="form-control" type="text" name="scope" placeholder="scope1">
				</div>
				<div class="form-group">
					<label>response_type</label>
					<input class="form-control" type="text" name="response_type" placeholder="code">
				</div>
				<div class="form-group">
					<label>state</label>
					<input class="form-control" type="text" name="state" placeholder="123456">
				</div>
				<button class="btn btn-primary" type="submit">前往授权</button>
			</form>
		</div>	
	</div>
</div>
@stop