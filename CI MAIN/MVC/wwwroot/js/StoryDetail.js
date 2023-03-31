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


const countEl = document.getElementById('count');

updateVisitCount();

function updateVisitCount() {

    //Develop a namespace and key for your API, in my case "megs" is my namespace and "count" is my key
    fetch('https://api.countapi.xyz/update/megs/count/?amount=1')
        .then(res => res.json())
        .then(res => {
            countEl.innerHTML = res.value;
        })
}



function Recommend(btn) {
    var userMail = btn.value;
    var currentURL = window.location.href;
    console.log(btn);
    btn.innerText = "Sending...";
    btn.disabled = true;
    $.ajax({
        url: '/StoryListing/Recommend',
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