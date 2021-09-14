$('ul.nav-menu li.dropdown > a').on('click', function (e) {
    e.stopImmediatePropagation();
});

$('#mobile-nav ul li.dropdown > a').on('click', function (e) {
    debugger;
    $(this + ' + dropdown-menu').toggleClass('show');
    e.stopImmediatePropagation();
});