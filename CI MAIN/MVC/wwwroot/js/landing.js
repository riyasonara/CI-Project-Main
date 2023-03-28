


function showList(e) {
 
    var $gridCont = $('.grid-container');
    e.preventDefault();
    $gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
}
function gridList(e) {
    //localStorage.setItem("view", "grid");
    var $gridCont = $('.grid-container')
    e.preventDefault();
    $gridCont.removeClass('list-view');
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);



function AddtoFav() {
    const params = new URLSearchParams(window.location.search);
    const query = params.get('missionid');
    $.ajax({
        url: "/User/AddToFav",
        data: { missionId: query },
        success: function (result) {
            if (result.isFav) {
                document.getElementById("addtofav").style.color = "black";
            }
            else {
                document.getElementById("addtofav").style.color = "#F88634";
            }

        },
        error: function () {
            alert("Error : Mission has not been liked");
        }
    });
}