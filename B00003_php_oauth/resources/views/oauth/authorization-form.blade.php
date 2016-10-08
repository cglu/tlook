@extends('master') @section('content')
<div class="container">
	<div class="row">
		<div class="col-md-offset-2 col-md-8">
			<div class="well">
				<p>您即将授权{{ $client->getName() }}应用获取您以下数据：</p>
				<ul>
					@foreach($params['scopes'] as $scope)

					<li>{{ $scope->getDescription() }}</li> @endforeach
				</ul>
			</div>
			<form role="form" action="/oauth/authorize" method="get">
				<input class="form-control" type="hidden" name="client_id"
					value="{{ $params['client_id'] }}"> <input class="form-control"
					type="hidden" name="redirect_uri"
					value="{{ $params['redirect_uri'] }}"> <input class="form-control"
					type="hidden" name="scope" value="{{ $params['scope'] }}"> <input
					class="form-control" type="hidden" name="response_type"
					value="{{ $params['response_type'] }}"> <input class="form-control"
					type="hidden" name="state" value="{{ $params['state'] }}"> <input
					type="hidden" name="_token" value="<?php echo csrf_token(); ?>"> <input
					type="hidden" name="approve" value="1">
					 <input type="hidden" name="_method" value="POST">
					 <input type="hidden" name="action" value="callback">
				<button class="btn btn-primary" type="submit">同意授权</button>
				<button class="btn btn-default">取消</button>
			</form>
		</div>
	</div>
</div>
@endsection
