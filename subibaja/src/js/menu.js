$(function() {
  slider = $("#slider").slideReveal({
    push: false,
    position: "right",
    speed: 600,
    trigger: $("#trigger"),
    show: function(obj) {
      $("#main-container").addClass("disabled");
    },
    shown: function(obj) {},
    hide: function(obj) {
      $("#main-container").removeClass("disabled");
    },
    hidden: function(obj) {}
  });
});
