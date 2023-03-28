function ratemission(starId, missionId, id) {
    $.ajax({
        url: '/User/AddRating',
        type: 'POST',
        data: { missionId: missionId, id: id, rating: starId },
        success: function (result) {
            //if (parseInt(result.ratingExists.rating, 10)) {
            //    //     Update the heart icon to show that the mission has been liked
            //    for (i = 1; i <= result.ratingExists.rating; i++) {

            //        var starbtn = document.querySelector('.star-' + i);
            //        console.log(starbtn);
            //        starbtn.style.color = "#F88634";
            //    }
            //    for (i = result.ratingExists.rating + 1; i <= 5; i++) {

            //        var starbtn = document.querySelector('.star-' + i);;
            //        console.log(starbtn);
            //        starbtn.style.color = "black";
            //    }
            //} else {
            //    //    Update the heart icon to show that the mission has been unliked
            //    for (i = 1; i <= parseInt(result.newRating.rating, 10); i++) {

            //        var starbtn = document.getElementById(String(i));

            //        starbtn.style.color = "#F88634";
            //    }
            //    for (i = parseInt(result.newRating.rating, 10) + 1; i <= 5; i++) {

            //        var starbtn = document.getElementById(String(i));

            //        starbtn.style.color = "black";
            //    }
            //}
            $('#givenrating').html($(result).find('#givenrating').html());
        },
        error: function () {
            // Handle error response from the server, e.g. show an error message to the user
            alert('Error: Could not like mission.');
        }
    });
}
