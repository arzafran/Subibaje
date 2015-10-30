$(function() {
  slider = $("#slider").slideReveal({
    push: false,
    position: "right",
    speed: 600,
    trigger: $("#trigger"),
    show: function(obj) {
      $("#main-container").addClass("disabled");
      $(".btn-fab").animate({ 
        right: "+=200",
      },500);
    },
    shown: function(obj) {},
    hide: function(obj) {
      $("#main-container").removeClass("disabled");
      $(".btn-fab").animate({ 
        right: "-=200",
      },500);
    },
    hidden: function(obj) {}
  });
});
