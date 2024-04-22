"use strict";
$.noConflict();
var $ = jQuery;

$( document ).ready(function($){

    // Dropdown Selection Js
    $( '#pluginslist' ).on("click",  function () {
        var id = jQuery('#pluginslist').val(); 
        jQuery('.pluginwrap').hide(); 
        jQuery('#' + id).show(); return;
    });
    
    // Animate to Particular section Js
    var $window = $(window)
        $('section [href^=#]').on("click", function(e) {
        e.preventDefault()
    });
    $('.clickable').on("click", function() {
        var pos = $($(this).attr("data-section")).offset().top;
        jQuery('html, body').animate({
            scrollTop: pos
        }, 'slow');
    });

    // Animate fom Particular section to top Js
    $('.page-header > a').on("click", function() {
        jQuery('html, body').animate({
            scrollTop: 0
        }, 'slow');
    });

});