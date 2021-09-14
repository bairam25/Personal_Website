/* ----------------------------------------------------------- */
/*  Style Switcher
/* ----------------------------------------------------------- */
$(document).ready(function () {
	$('.style-switch-button').click(function () {
		$('.style-switch-wrapper').toggleClass('active');
	});
	$('a.close-styler').click(function () {
		$('.style-switch-wrapper').removeClass('active');
	});
});

jQuery(document).ready(function ($) {
	var links = window.document.getElementsByTagName('link');

	// Color Changer
	$("#preset1").click(function () {
		$(links).each(function () {
			var tagLink = $(this).attr("href");  // this is your href for the link tag in the loop
			if (tagLink.includes("css/style")) {
				$(this).attr("href", "css/style.css");
			} else if (tagLink.includes("css/main")) {
				$(this).attr("href", "css/main.css");
			} else if (tagLink.includes("App_Themes/")) {
				$(this).attr("href", "App_Themes/App_Theme/StyleSheet.css");
			}
		});
	});

	$("#preset2").click(function () {
		$(links).each(function () {
			var tagLink = $(this).attr("href");  // this is your href for the link tag in the loop
			if (tagLink.includes("css/style")) {
				$(this).attr("href", "css/style2.css");
			} else if (tagLink.includes("css/main")) {
				$(this).attr("href", "css/main2.css");
			} else if (tagLink.includes("App_Themes/")) {
				$(this).attr("href", "App_Themes/App_Theme2/StyleSheet.css");
			}
		});
	});

	//$("#preset3").click(function () {
	//	$(links).each(function () {
	//		var tagLink = $(this).attr("href");  // this is your href for the link tag in the loop
	//		if (tagLink.includes("css/style")) {
	//			$(this).attr("href", "css/style3.css");
	//		} else if (tagLink.includes("css/main")) {
	//			$(this).attr("href", "css/main3.css");
	//		} else if (tagLink.includes("App_Themes/")) {
	//			$(this).attr("href", "App_Themes/App_Theme3/StyleSheet.css");
	//		}
	//	});
	//});

});