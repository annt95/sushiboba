/**
 * Created by Sushant on 12/14/2017.
 */
$(document).ready(function() {
	navInit();
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

/*-------------------------------- Functions Ends --------------------------------*/
