///<reference path="../lib/jquery/dist/jquery.js" />

$(function () {
  $('h5').on('click', function () {
    const id = $(this).data('id');
    $('.modal-body').load(`?handler=PropertyDetails&id=${id}`);
  });
  $('#book').on('click', function () {
    const data = {
      startdate: $('#StartDate').val(),
      enddate: $('#EndDate').val(),
      numberofguests: $('#NumberOfGuests').val(),
      property: {
        id: $('#Property_Id').val()
      }
    };
    $.ajax({
      url: '/properties/booking',
      method: "post",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(data)
    })
    .done(function (response) {
      alert(`Your stay will cost ${response.totalCost}`);
    });
  });
});