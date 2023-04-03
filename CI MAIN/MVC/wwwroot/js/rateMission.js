function ratemission(starId, missionId, id) {
    $.ajax({
        url: '/User/AddRating',
        type: 'POST',
        data: { missionId: missionId, id: id, rating: starId },
        success: function (result) {
           
            $('#givenrating').html($(result).find('#givenrating').html());
        },
        error: function () {
            // Handle error response from the server, e.g. show an error message to the user
            alert('Error: Could not rate mission.');
        }
    });
}
