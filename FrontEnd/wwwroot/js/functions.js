/**
 * Created by Sushant on 12/14/2017.
 */
$(document).ready(function() {
	
});

/*------------------------------- Functions Starts -------------------------------*/
function navInit() {
	$('.navigation_toggle').click(function() {
		$('body').toggleClass('modal-open header-down');
	});
	$('.tcon-menu--xcross').click(function() {
		$(this).toggleClass('tcon-transform');
	});
	$('#header-wrapper ul li ul')
		.parent()
		.addClass('hasSubChild');
	$('#header-wrapper .nav-container .hasSubChild').on('click', function() {
		if ($(this).hasClass('open')) {
			$(this).removeClass('open');
		} else {
			$('#header-wrapper .nav-container .hasSubChild').removeClass('open');
			$(this).addClass('open');
		}
	});
	// menu wokto
	$('.cs-popup_close, .cs-popup_link').click(function(e) {
		e.preventDefault();
		$('.cs-popup').fadeOut();
	});
	$('.mp-toggle-menu').click(function() {
		$(this).toggleClass('active');
		$('.mp-menu-mb').slideToggle();
	});

	function initialize() {
		var input = document.getElementById('location');
		new google.maps.places.Autocomplete(input);
	}

	google.maps.event.addDomListener(window, 'load', initialize);
}

$(window).scroll(function() {
	var winHeight = $(window).height();
	var offset = 0.6;

	var scrollTop = $(window).scrollTop();
	var visibleArea = scrollTop + winHeight * offset;

	$('.animation-area').each(function() {
		if ($(this).offset().top < visibleArea) {
			$(this)
				.find('.ani-zoom-item')
				.addClass('normal');
			$(this)
				.find('.splash-text')
				.addClass('normal');
			$(this)
				.find('.ani-fade-item')
				.addClass('normal');
			$(this)
				.find('.ani-left')
				.addClass('normal');
			$(this)
				.find('.ani-right')
				.addClass('normal');
			$(this)
				.find('.ani-top')
				.addClass('normal');
		} else {
			$(this)
				.find('.ani-zoom-item')
				.removeClass('normal');
			$(this)
				.find('.splash-text')
				.removeClass('normal');
			$(this)
				.find('.ani-fade-item')
				.removeClass('normal');
			$(this)
				.find('.ani-left')
				.removeClass('normal');
			$(this)
				.find('.ani-right')
				.removeClass('normal');
			$(this)
				.find('.ani-top')
				.removeClass('normal');
		}
	});

	if ($('.animation-area-sequence').length) {
		if ($('.animation-area-sequence').offset().top < visibleArea) {
			$('.animation-area-sequence .ani-sequence-item').each(function(i) {
				setTimeout(function() {
					$('.animation-area-sequence .ani-sequence-item')
						.eq(i)
						.addClass('normal');
				}, 80 * (i + 1));
			});
		} else {
			$('.animation-area-sequence .ani-sequence-item').removeClass('normal');
		}
	}
});
$(window).bind('scroll', function() {
	if ($(window).scrollTop() > 10) {
		$('.mp-header').addClass('fixed');
	} else {
		$('.mp-header').removeClass('fixed');
	}
});
(function ($) {
	$(document).ready(function () {
		navInit();
		// for modal
		$('input[type=radio][name=optdeli]').change(function () {
			if (this.value == 'Pickup') {
				debugger;
				let sumprice = document.getElementById("ttPrice").innerHTML.split(' ')[0];
				document.getElementById('order-fee').style.visibility = "hidden";
				document.getElementById('location').style.display = 'none';
				document.getElementById("totalcheckout").textContent = sumprice + ' Kr'; 
			}
			else if (this.value == 'Ship') {
				let sumprice = document.getElementById("ttPrice").innerHTML.split(' ')[0];
				document.getElementById('order-fee').style.visibility = "";
				document.getElementById('location').style.display = 'block';
				document.getElementById("totalcheckout").textContent = parseInt(sumprice) + 249 + ' Kr'; 
			}
		});
		function checkDeli() {

			if (document.getElementById('optionsPickup').checked) {
				let sumprice = document.getElementById("ttPrice").innerHTML.split(' ')[0];
				document.getElementById('order-fee').style.visibility = "hidden";
				document.getElementById('location').style.display = 'none';
				document.getElementById("totalcheckout").textContent = sumprice + ' Kr'; 
			}
			else {
				let sumprice = document.getElementById("ttPrice").innerHTML.split(' ')[0];
				document.getElementById('order-fee').style.visibility = "";
				document.getElementById('location').style.display = 'block';
				document.getElementById("totalcheckout").textContent = parseInt(sumprice) + 249 + ' Kr'; 
			}
		}
		$('.button-modal').click(function () {
			$.ajax({
				type: "GET",
				url: '/Ajax/Menu/ListStringItem',
				success: function (odata) {
					if (odata == null) {
						alert('Your cart is empty');
					} else {
						//$("span").removeData("dtItem");
						$.ajax({
							type: "GET",
							url: '/Ajax/Menu/checkCart',
							success: function (odata) {
								document.getElementById("coutCart").textContent = odata.count;
								document.getElementById("cCart").textContent = odata.count;
								document.getElementById("ttPrice").textContent = odata.totalPrice + ' Kr';
								checkDeli();
							}
						});
						$('#dtItem').empty();
						$("#dtItem").append(odata);
						$('body').addClass('.cs-modal--open');
						$('.cs-modal').fadeIn(300);
						checkDeli();
						//document.getElementById("optionsRadios1").checked = true;
					}					
				}
			});
			
		});
		$('.cs-modal').click(function (e) {
			if (e.target != this) return;
			$(this).hide();
		});
		$('.js-modal--close').click(function (e) {
			e.preventDefault();
			$('.cs-modal').hide();
		});
		jQuery.validator.addMethod("rgphone", function (value, element) {
			return this.optional(element) || /^[0-9]/.test(value);
		}, "phonenumber incorrect");
		function getFormData($form) {
			var unindexed_array = $form.serializeArray();
			var indexed_array = {};
			$.map(unindexed_array, function (n, i) {
				indexed_array[n['name']] = n['value'];
			});

			return indexed_array;
		}
		$('.btnSubmitOrder').click(function (e) {
			e.preventDefault();
			if ($("#frmOrder").valid() == true) {
				var data = getFormData($("#frmOrder"));
				if (data.optdeli == "Ship" && data.location.trim() == "") {
					alert('Please fill your location');
					return;
				}
				debugger;
				$.ajax({					
					type: "POST",
					url: '/Ajax/Menu/AddOrder',
					data: data,
					success: function (odata) {
						debugger;
						if (odata.errorcode == 0) {
							window.location.href = "/Order/" + odata.msg;
						}
						else {
							alert(odata.msg);
						}
					}

				});

			}
					 });
		$(document).on('click', '.additem', function () {			
			event.preventDefault();
			var id = $(this).data('id');
			var price = $(this).data('price');
			var oldvalue = $('input[data-id="' + id + '"]').val();
			var newvalue = (parseInt(oldvalue) + 1).toString();
			document.getElementById(id).value = newvalue;

			//$('#' + id + '').value = newvalue;
			//var price = Number($(this).data('price'));
			//var id = 16;
			var count = 1;
			$.ajax({
				type: "POST",
				url: '/Ajax/Menu/addCart',
				data: { id: id, price: price },
				success: function (odata) {
					document.getElementById("coutCart").textContent = odata.count; 
					document.getElementById("cCart").textContent = odata.count; 
					document.getElementById("ttPrice").textContent = odata.totalPrice; 

				}
			});
		});
		$(document).on('click', '.subitem', function () {
			event.preventDefault();
			var id = $(this).data('id');
			document.getElementById(id).value = "0";
			$.ajax({
				type: "POST",
				url: '/Ajax/Menu/deleteItemCart',
				data: { id: id},
				success: function (odata) {
					document.getElementById("coutCart").textContent = odata.count;
					document.getElementById("cCart").textContent = odata.count;
					document.getElementById("ttPrice").textContent = odata.totalPrice; 
				}
			});
		});
		$("#frmOrder").validate({
			rules: {
				fphone: { required: true, number: true, rgphone: true, minlength: 5 },
				fname: { required: true, minlength: 2 }
			},
			messages: {
				fphone: { required: "Input your phonenumber", number: "Number only" },
				fname: { required: "Input your name", minlength: "Too short" }
			},
			submitHandler: function () {
				//$('.btnCheckInfoCus').attr('disabled', true);
				//var CustomerID = $('#CustomerID').val();
				//var CustomerPhone = $('#CustomerPhone').val();
				//var ProviderCode = $("#slcProviderCode option:selected").val();
				return false;
			}
		});


	});
})(window.jQuery);
/*-------------------------------- Functions Ends --------------------------------*/
