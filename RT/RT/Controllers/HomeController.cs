using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EdgeJs;
using System.Threading.Tasks;


namespace RT.Controllers
{
	public class HomeController : ApplicationBaseController
	{
		//static Func<object, Task<object>> func = Edge.Func(@"
		//	var current = 0;

		//	return function (data, callback){
		//		current += data;
		//		callback(null, current);
		//	}
		//");



		//Linked up. Need to change it to a relative path.
		//static Func<object, Task<object>> outsideFunc = Edge.Func(@"return require('C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\Scripts\\myfunc.js')");


		public async Task<ActionResult> Index()
		{


			//THIS IS THE CODE THAT WILL ACTIVATE THE WEB SCRAPPER 
			
			//try
			//{
			//	ViewBag.Message = await outsideFunc("Hello");
				
			//}
			//catch (AccessViolationException e )
			//{
			//	Console.WriteLine("There was an error!");
			//	Console.WriteLine(e);
			//}
			
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}