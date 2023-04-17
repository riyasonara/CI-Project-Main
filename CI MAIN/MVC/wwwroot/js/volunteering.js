
let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("demo");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

function openCity(evt, cityName) {
    var i, x, tablinks;
    x = document.getElementsByClassName("city");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < x.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active-tab", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active-tab";
}

var debounceTimer;

function debounce(func, delay) {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(func, delay);
}
document.getElementById("search-bar").addEventListener("input", function () {
    debounce(function () {
        search(document.getElementById("search-bar").value);
    }, 500); // adjust the delay time as needed
});

function search(query) {
    // Get the current URL
    let url = window.location.href;

    let separator = url.indexOf('?') !== -1 ? '&' : '?';

    // Check if the searchQuery parameter already exists in the URL
    if (url.includes('searchQuery=')) {
        // Replace the value of the searchQuery parameter
        url = url.replace(/searchQuery=([^&]*)/, 'searchQuery=' + query);
    } else {
        // Append the parameter to the URL
        url += separator + 'searchQuery=' + query;
    }

    // Navigate to the updated URL
    window.location.href = url;

}


    $(function () {
        $(".rating .star").click(function () {
            var value = $(this).index() + 1;
            var itemId = $(this).parent().data("item-id");
            $.ajax({
                url: "/User/VolunteeringMission",
                type: "POST",
                data: { itemId: itemId, value: value },
                success: function (result) {
                    $(".rating").html(result);
                }
            });
        });
        });

function AddtoFav(missionid, id) {
    const params = new URLSearchParams(window.location.search);
    const query = params.get('missionid');
    $.ajax({
        url: "/User/AddToFav",
        data: { missionid: missionid,id:id },
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


function PostComment() {
    var textBox = document.getElementById("comment");
    var commentVal = textBox.value;
    const params = new URLSearchParams(window.location.search);
    const query = params.get('missionid');
    $.ajax({
        url: "/User/PostComment",
        data: { missionId: query, commentVal: commentVal },
        success: function (result) {
            $('.commentdiv').html($(result).find('.commentdiv').html());
            //window.location.reload();
        },
        error: function () {
            alert("Error : Mission has not been liked");
        }
    });
}


function Recommend(btn) {
    var userMail = btn.value;
    var currentURL = window.location.href;
    console.log(btn);
    btn.innerText = "Sending...";
    btn.disabled = true;
    $.ajax({
        url: '/User/Recommend',
        type: 'POST',
        data: { targetURL: currentURL, userMail: userMail },
        success: function (result) {
            btn.innerText = "Shared";
            btn.classList.remove("btn-outline-primary");
            btn.classList.add("btn-outline-success");
            btn.disabled = true;
        }
    });
}

function apply(mid, uid) {
    $.ajax({
        url: '/User/apply',
        type: 'POST',
        data: { MissionId: mid, UserId: uid },
        success: function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Applied',
                text: 'Applied Successfully',

            });
            window.location.href = 'VolunteeringMission?missionid=' + mid;

        },
        error: function (result) { }
    });
}