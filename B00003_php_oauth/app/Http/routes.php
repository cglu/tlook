<?php
use Illuminate\Support\Facades\Request;
use Illuminate\Support\Facades\Session;
use Illuminate\Support\Facades\Auth;

/*
 * |--------------------------------------------------------------------------
 * | Routes File
 * |--------------------------------------------------------------------------
 * |
 * | Here is where you will register all of the routes in an application.
 * | It's a breeze. Simply tell Laravel the URIs it should respond to
 * | and give it the controller to call when that URI is requested.
 * |
 */

/*
 * |--------------------------------------------------------------------------
 * | Application Routes
 * |--------------------------------------------------------------------------
 * |
 * | This route group applies the "web" middleware group to every route
 * | it contains. The "web" middleware group is defined in your HTTP
 * | kernel and includes session state, CSRF protection, and more.
 * |
 */

Route::group([
    'middleware' => [
        'web'
    ]
], function () {
    
    Route::get('oauth/authorize', [
        
        'middleware' => [
            'check-authorization-params',
            'auth'
        ],
        function () {
            $action = '';
            if (Request::has('action')) {
                // 不做
                $action = Request::get('action');
            } else {
                $action = '';
            }
            if (Request::get('action') == 'callback') {
                $params = Authorizer::getAuthCodeRequestParams();
                $params['user_id'] = Auth::user()->id;
                $redirectUri = '/';
                // If the user has allowed the client to access its data, redirect back to the client with an auth code.
                if (Request::has('approve')) {
                    
                    $redirectUri = Authorizer::issueAuthCode('user', $params['user_id'], $params);
                }
                
                // If the user has denied the client to access its data, redirect back to the client with an error message.
                if (Request::has('deny')) {
                    $redirectUri = Authorizer::authCodeRequestDeniedRedirectUri();
                }
               
                return Redirect::to($redirectUri);
               
            } else {
                $authParams = Authorizer::getAuthCodeRequestParams();
                $formParams = array_except($authParams, 'client');
                
                $formParams['client_id'] = $authParams['client']->getId();
                $formParams['scope'] = implode(config('oauth2.scope_delimiter'), array_map(function ($scope) {
                    return $scope->getId();
                }, $authParams['scopes']));
                
                return view('oauth.authorization-form', [
                    'params' => $formParams,
                    'client' => $authParams['client']
                ]);
            }
        }
    ]);
    Route::post('oauth/access_token', function () {
        return Response::json(Authorizer::issueAccessToken());
    });
    
    Route::get('/', function () {
        
        return View::make('home');
    });
    Route::get('/login', function () {
        // User::firstOrCreate(array('user_login'=>'demo', 'password'=>'demo'));
        return View::make('login');
    });
    
    Route::post('/login', function () {
        $data = \Illuminate\Support\Facades\Request::only('email', 'password');
        
        if (Auth::attempt($data)) {
            
            $data = Session::get('oauth_request_pars');
            $data = http_build_query($data);
            return redirect('oauth/authorize?' . $data);
        } else {
            echo "登陆失败";
        }
    });
    
    Route::get('/resource', array(
        'before' => 'oauth',
        function () {
            return '我是小球球，这里没有任何权限控制';
        }
    ));
    
    Route::get('/resource/scope1', array(
        'middleware' => [
            'oauth:scope1'
        ],
        function () {
            return '我是小球球，只有申请了scope1权限的client才能访问';
        }
    ));
    
    Route::get('/resource/scope2', array(
        'before' => 'oauth:scope2',
        function () {
            return '我是小球球，只有申请了scope2权限的client才能访问';
        }
    ));
    
    Route::get('/callback', function () {
        if (\Illuminate\Support\Facades\Request::has('code')) {
            return View::make('callback');
        }
    });
    //
});