"use strict";
$.noConflict();
var $ = jQuery;

$( document ).ready(function($){

  // smooth scroll animation for banner & menu links js 
    var headerHeight = $("#header").height();
    $('.jumplinks a[href*="#"]:not([href="#"])').on("click", function() {
      if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
        var target = $(this.hash);
        target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
        if (target.length) {
          $('html, body').animate({
            scrollTop: target.offset().top - headerHeight
          }, 1000);
          return false;
        }
      }
    });

    
    // adds active class in menu js 
    $('.navbar-nav li a').on("click", function(e) {
        $('.navbar-nav li').removeClass('active');
        var $parent = $(this).parent();
        if (!$parent.hasClass('active')) {
            $parent.addClass('active');
        }
    }); 

  
  // Main Banner Slider Js
     $(".banner_slider").slick({
      dots: true,
        infinite: true
     });


  // Owl Slider for team section
  $('.teamslider').owlCarousel({
     items:3,
     mouseDrag:true,
     margin:30,
     nav:true,
     dots:false,
     responsive:{  
        767:{  
           items:3
        },
        500:{  
           items:2
        },
         20:{  
           items:1
        }
     }  
  }); 


  // equal height  js for customer section
  $(".customer_slider").height($(".customerBox").height());
  

  // Owl Slider for Partner logos
  $('.logoSlide').owlCarousel({
     items:4,
     mouseDrag:true,
     margin:30,
     nav:true,
     dots:false,
     responsive:{  
        1000:{  
           items:4
        },
        767:{  
           items:3
        },
        600:{  
           items:2
        },
         20:{  
           items:1
        }
     }  
  }); 


  // Owl Slider for Customer Section
    $('.custSlider').owlCarousel({
       items:1,
       mouseDrag:true,
       margin:30,
       nav:true,
       dots:false,
       loop: true,
       responsive:{  
          1000:{  
             items:1
          },
          767:{  
             items:1
          },
          600:{  
             items:1
          },
           20:{  
             items:1
          }
       }
    }); 


    // NAVBAR CLOSE ICON In Smaller Devices
    $(".navbar-toggle").on("click", function () {
        $(this).toggleClass("active");
        $("#header").toggleClass("headClr");
        $("body").toggleClass("popup-open");
    });


    $('.main-menu ul li a').on("click", function(){
        $("body").removeClass("popup-open");
    });


    // NAVBAR OPEN/CLOSE In Smaller Devices
    function resMenu() {
        if ($(window).width() < 1200) {

            $('.main-menu ul li a').on("click", function () {
                $(".navbar-collapse").removeClass("in"); 
                $(".navbar-toggle").addClass("collapsed").removeClass("active");
                $("#header").removeClass("headClr");            
            });
      }
    }

    resMenu();


    // Fancybox Popup for Portfolio
    $(".grouped_portfolio").fancybox({
        helpers: {
            overlay: {
                locked: false
            }
        }
    });


    // Back To top Js
    var offset = 300,
        offset_opacity = 1200,
        scroll_top_duration = 700,
        $back_to_top = $('.back-to-top');

    $(window).scroll(function(){

        ( $(this).scrollTop() > offset ) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
        if( $(this).scrollTop() > offset_opacity ) { 
            $back_to_top.addClass('cd-fade-out');
        }

        var scroll = $(window).scrollTop();
        if (scroll >= 150) {
          $("#header").addClass("fixed");
        } 
        else {
          $("#header").removeClass("fixed");
        }

    });

    $back_to_top.on('click', function(event){
        event.preventDefault();
        $('body,html').animate({
            scrollTop: 0 ,
            }, scroll_top_duration
        );
    });


  // wow js for animation
  new WOW().init();


  // Progress Bar Animation
  $("#box").waypoint({
    handler: function(direction) {
      $('.box-progress-bar').each(function(){
        var width = $(this).data('progress');
        var to = $(this).data('progress')
        $(this).find('.bar').animate({
          width: width + "%"
        }, 500, "swing");

        $(this).find('.text').animate({
          left: width + "%"
        }, {
          duration: 5000,
          easing: 'swing',
          step: function (count) {
            $(this).text(Math.ceil( count ));
          }
        });
      });
    },
    offset: '50%'
  });


    // init Isotope Js for Portfolio
    var $grid = $('.grid').isotope({
      itemSelector: '.element-item',
      layoutMode: 'fitRows'
    });
    var filterFns = {
      numberGreaterThan50: function() {
        var number = $(this).find('.number').text();
        return parseInt( number, 10 ) > 50;
      },
      ium: function() {
        var name = $(this).find('.name').text();
        return name.match( /ium$/ );
      }
    };
    $('.portfolio-filter').on( 'click', 'a', function() {
      var filterValue = $( this ).attr('data-filter');
      // use filterFn if matches value
      filterValue = filterFns[ filterValue ] || filterValue;
      $grid.isotope({ filter: filterValue });
    });
    $('.nav-tabs-light').each( function( i, buttonGroup ) {
      var $buttonGroup = $( buttonGroup );
      $buttonGroup.on( 'click', 'li', function() {
        $buttonGroup.find('.active').removeClass('active');
        $( this ).addClass('active');
        return false;
      });
    });

    $grid.imagesLoaded().progress( function() {
      $grid.isotope('layout');
    });
    
});