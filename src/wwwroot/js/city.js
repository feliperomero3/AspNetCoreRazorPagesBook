///<reference path="../lib/jquery/dist/jquery.js" />

$(function () {
  $('h5').on('click', function () {
    const id = $(this).data('id');
    $('.modal-body').load(`?handler=PropertyDetails&id=${id}`);
  });
});