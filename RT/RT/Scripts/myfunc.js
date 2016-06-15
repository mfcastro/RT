"Use Strict"

const scrapeIt = require("scrape-it");
var jsonfile = require('jsonfile')

module.exports = function (data, callback) {

	callback(null, scraper(data));
};





var scraper = function (data) {
	var file = 'C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\\Scripts\\data.json';
	//var recipeURL = 'http://www.foodnetwork.com/recipes/rachael-ray/radicchio-pasta-salad-recipe.html';
	//var recipeURL = 'http://www.foodnetwork.com/recipes/rachael-ray/tomato-onion-and-cucumber-salad-recipe.html';
	var recipeURL = data;
	if (recipeURL.includes('allrecipes')) {
		console.log('All Recipes Recipe')

		//--------------------------------------------------------------------------------------------    

		scrapeIt(recipeURL,
		{
			recipeTitle: 'h1',
			ingredients: {
				listItem: '.recipe-ingred_txt '
			},
			directions: {
				listItem: '.directions--section__steps .step .recipe-directions__list--item'
			}
			//recipeImage: {
			//	selector: '.hero-photo__image img',
			//	attr: 'src'
			//}
		}).then(page => {
			//console.log(page) 
			//console.log(page.recipeTitle)
			//page['url'] = recipeURL;
			//var file = 'recipe.json';
			page['recipeURL'] = recipeURL;
			jsonfile.writeFile(file, page, { spaces: 2 }, function (err) {
				console.error(err)
			})
			return page;
		});

		//--------------------------------------------------------------------------------------------   

	}
	else if (recipeURL.includes('foodnetwork')) {
		console.log('Food Network Recipe')


		//--------------------------------------------------------------------------------------------

		scrapeIt(recipeURL,
	{
		recipeTitle: 'h1',
		ingredients: {
			listItem: '.ingredients-instructions .bd .col8 ul li' // ul li .box-block'
		},
		directions: {
			listItem: '.directions .recipe-directions-list p'
		},
		recipeImage: {
			selector: '.lightbox-recipe-video .ico-wrap img',
			attr: 'src'
		}
	}).then(page => {
		//console.log(page) 

		//var file = 'recipe.json';
		//var file = 'C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\\Scripts\\data.json'
		page['recipeURL'] = recipeURL;

		jsonfile.writeFile(file, page, { spaces: 2 }, function (err) {
			console.error(err)
		})

		return page;
	});


		//--------------------------------------------------------------------------------------------
	} else {
		//Schema.org Scraper

		scrapeIt(recipeURL,
	 {
	 	recipeTitle: 'h1',
	 	ingredients: {
	 		listItem: '.ingredient' // ul li .box-block'
	 	},
	 	directions: {
	 		listItem: '.instruction'
	 	}
	 }).then(page => {
	 	//console.log(page) 

	 	//var file = 'recipe.json';
	 	var file = 'C:\\Users\\Marco Castro\\Desktop\\RT\\RT\\RT\\\Scripts\\data.json'
	 	page['recipeURL'] = recipeURL;
	 	jsonfile.writeFile(file, page, { spaces: 2 }, function (err) {
	 		console.error(err)
	 	})

	 	return page;

	 });
	}
}