$(document).ready(function () {

	var ingredient_max_fields = 15; //maximum input boxes allowed
	var direction_max_fields = 15; //maximum input boxes allowed
	var wrapper = $(".input_fields_wrap"); //Fields wrapper
	var add_button = $(".add_field_button"); //Add button ID

	var ingredient_initial = 5; //initlal text box count
	var direction_initial = 5;
	$(add_button).click(function (e) { //on add input button click
		e.preventDefault();
		if (ingredient_initial < ingredient_max_fields) { //max input box allowed
			ingredient_initial++; //text box increment
			//$(wrapper).append('<div><input type="text" name="mytext[]"/><a href="#" class="remove_field">Remove</a></div>'); //add input box



			$(wrapper).append('<div class="form-group">'
						+'<div class="col-md-10">'
							+ '<input class="form-control text-box single-line" id="Ingredients_RecipeIngredient' + ingredient_initial+ '" name="Ingredients.RecipeIngredient' + ingredient_initial+ '" type="text" value="" />'
							+ '<span class="field-validation-valid text-danger" data-valmsg-for="Ingredients.RecipeIngredient' + ingredient_initial+ '" data-valmsg-replace="true"></span>'
							+ '<a href="#" class="remove_field">Remove</a>'
						+'</div>'
					+'</div>');

		}
	});

	$(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
		e.preventDefault(); $(this).parent('div').remove(); ingredient_initial--;
	})



	$('.add_direction_field_button').click(function (e) { //on add input button click
		e.preventDefault();
		if (direction_initial < direction_max_fields) { //max input box allowed
			direction_initial++; //text box increment
			//$(wrapper).append('<div><input type="text" name="mytext[]"/><a href="#" class="remove_field">Remove</a></div>'); //add input box



			$(".direction_input_fields_wrap").append('<div class="form-group">'
						+'<div class="col-md-10">'
							+ '<input class="form-control text-box single-line" id="Directions_RecipeDirection' + direction_initial+ '" name="Directions.RecipeDirection' + direction_initial+ '" type="text" value="" />'
							+ '<span class="field-validation-valid text-danger" data-valmsg-for="Directions.RecipeDirection' + direction_initial+ '"  data-valmsg-replace="true"></span>'
							+ '<a href="#" class="remove_field">Remove</a>'
						+'</div>'
					+'</div>');

		}
	});

	$('.direction_input_fields_wrap').on("click", ".remove_field", function (e) { //user click on remove text
		e.preventDefault(); $(this).parent('div').remove(); direction_initial--;
	})


});



