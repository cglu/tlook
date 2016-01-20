//前提
	Mock.mock('http://g.cn','get',function (options){
		alert(options.url);
		return {'user':'lisi','age':18};
	});

	
 
