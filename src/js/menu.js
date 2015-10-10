$(function() {
  slider = $("#slider").slideReveal({
    // width: 100,
    // push: false,
    position: "right",
    speed: 600,
    trigger: $("#trigger"),
    // autoEscape: false,
    show: function(obj) {
      console.log(obj);
    },
    shown: function(obj) {
      console.log(obj);
    },
    hide: function(obj) {
      console.log(obj);
    },
    hidden: function(obj) {
      console.log(obj);
    }
  });

  slider2 = $("#slider2").slideReveal({
    // width: 100,
    push: false,
    position: "left",
    speed: 600,
    trigger: $("#trigger2"),
    // autoEscape: false,
    top: 100
  });

  setTimeout(function() {
    slider.slideReveal("show", false);
  }, 1000);

  setTimeout(function() {
    slider.slideReveal("hide", false);
  }, 3000);
});

/*
Then you call:
$("#slider").slideReveal("show");
$("#slider").slideReveal("hide");
*/
