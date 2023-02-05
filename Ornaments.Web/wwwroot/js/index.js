
$('.multiple-items').slick({
  infinite: true,
  slidesToShow: 1,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 6000,
  speed: 800,
  arrows:true,
  dots: true,
  nextArrow: $('.control-next'),
  prevArrow: $('.control-prev'),
});


$('.Product-items').slick({
  infinite: true,
  slidesToShow: 4,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 6000,
  speed: 800,
  arrows:true,
  nextArrow: $('.Products-control-next'),
  prevArrow: $('.Products-control-prev'),
  responsive: [
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        slidesToShow: 3
      }
    },
	{
      breakpoint: 576,
      settings: {
        arrows: false,
        slidesToShow: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,        
        slidesToShow: 1
      }
    }
  ]
});

$('.brand-items').slick({
  arrows: false,
  infinite: true,
  slidesToShow: 5,
  slidesToScroll: 1,
  autoplay: true,
  autoplaySpeed: 3000,
  speed: 800,
  responsive: [
    {
      breakpoint: 768,
      settings: {
        arrows: false,
        slidesToShow: 4
      }
    },
	{
      breakpoint: 576,
      settings: {
        arrows: false,
        slidesToShow: 3
      }
    },
    {
      breakpoint: 480,
      settings: {
        arrows: false,        
        slidesToShow: 1
      }
    }
  ]
});

$('.search-icon').click(function() {
	if($('form-control').css('display', 'none')) {
		
		$('.form-control').removeClass('d-none');
		$('.search-box').addClass('inpute-show');
		$('.search-close-icon').css('display', 'block');
	}
});

$('.search-close-icon').click(function(){
	$('.form-control').addClass('d-none');
	$('.search-box').removeClass('inpute-show');
	$('.search-close-icon').css('display', 'none');
});

$('.navbar-toggler').click(function() {
		
	if($('.navbar-bg').css('display') == 'none') {
		$('.navbar-bg').css('display', 'block');
		$('.navbar-toggler').css('position', 'fixed');
		$('.navbar-toggler').css('margin-top', '20px');
		$('.change-icon').removeClass('navbar-toggler-icon');
		$('.change-icon').addClass('fas fa-times fa-lg');
		
		if($(window).width() > 360) {
			$('.navbar-toggler').css('right', '280px');
		}else {
			$('.navbar-toggler').css('right', '220px');
		}
		
	}else {
		$('.navbar-bg').css('display', 'none');
		$('.navbar-toggler').css('position', 'relative');
		$('.navbar-toggler').css('right', '0');
		$('.navbar-toggler').css('margin-top', '');
		$('.change-icon').addClass('navbar-toggler-icon');
		$('.change-icon').removeClass('fas fa-times fa-lg');
	}
});

$('.navbar-bg').click(function() {
	$('.navbar-collapse').removeClass('show');
	$('.navbar-bg').css('display', 'none');
	$('.navbar-toggler').addClass('collapsed');
	$('.navbar-toggler').css('right', '0');
	$('.change-icon').addClass('navbar-toggler-icon');
	$('.change-icon').removeClass('fas fa-times fa-lg');
});

