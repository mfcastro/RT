//$(document).ready(function () {
//	var max_fields = 10; //maximum input boxes allowed
//	var wrapper = $(".input_fields_wrap"); //Fields wrapper
//	var add_button = $(".add_field_button"); //Add button ID

//	var x = 1; //initlal text box count
//	$(add_button).click(function (e) { //on add input button click
//		e.preventDefault();
//		if (x < max_fields) { //max input box allowed
//			x++; //text box increment
//			$(wrapper).append('<div><input type="text" name="mytext[]"/><a href="#" class="remove_field form_control">Remove</a></div>');
//		}
//	});

//	$(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
//		e.preventDefault(); $(this).parent('div').remove(); x--;
//	})
//});

//var UsersModel = function () {
//	var users = ko.observableArray();

//	return {
//		users: users,
//		addUser: function () {
//			users.push({ name: ko.observable() });
//		}
//	};
//}

//var viewModel = new UsersModel();

//ko.applyBindings(viewModel, document.getElementById("wrapper")); // Don't use jQuery there!

//viewModel.addUser();