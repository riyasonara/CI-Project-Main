$(document).ready(function () {
    filter();

});
function filter(sortValue) {

    var Search = $("input[name='searchQuery']").val();
    //if (Search == '')
    //    Search = '';
    console.log(Search)


    var country = [];

    //$('#countryDropdown').find("input:checked").each(function (i, ob) {
    //    country.push($(ob).val());
    //});
    $("input[type='checkbox'][name='country']:checked").each(function () {
        country.push($(this).val());
    });
    console.log(country)


    var city = [];
    $("input[type='checkbox'][name='city']:checked").each(function () {
        city.push($(this).val());
    });
    console.log(city)

    var theme = [];
    $("input[type='checkbox'][name='theme']:checked").each(function () {
        theme.push($(this).val());
    });
    console.log(theme)

    $.ajax({
        url: "/User/navbarfilters",
        type: "POST",
        data: { 'search': Search, 'sortValue': sortValue, 'country': country, 'city': city, 'theme': theme},

        success: function (res) {
            
            $("#missioncards").html('');
            $("#missioncards").html(res);
        },
        error: function () {
            alert("some Error");
        }
    })
}


var cbs = document.querySelectorAll('input[type=checkbox]');
for (var i = 0; i < cbs.length; i++) {
    cbs[i].addEventListener('change', function () {
        if (this.checked) {
            addElement(this, this.value);
        }
        else {

            removeElement(this.value);
            console.log("unchecked");
        }
    });
}

function addElement(current, value) {
    let filtersSection = document.querySelector(".filters-section");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('ps-3');
    createdTag.classList.add('pe-1');
    createdTag.classList.add('me-2');
    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    let crossButton = document.createElement('button');
    crossButton.classList.add("filter-close-button");
    let cross = '&times;'


    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        console.log(elementToBeRemoved);
        console.log(current);
        elementToBeRemoved.remove();

        current.checked = false;




    })

    crossButton.innerHTML = cross;


    // let crossButton = '&times;'

    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);

}
function ClearAllElement() {

    var filtersSection = document.querySelector(".filters-section");

    for (var i = 0; i < filtersSection.length; i++) {
        filtersSection.pop();
    }

}


function removeElement(value) {

    let filtersSection = document.querySelector(".filters-section");

    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);

}







//---------------------------------------- Volunteering Mission js ----------------------------------

//let slideIndex = 1;
//showSlides(slideIndex);

//// Next/previous controls
//function plusSlides(n) {
//    showSlides(slideIndex += n);
//}

//// Thumbnail image controls
//function currentSlide(n) {
//    showSlides(slideIndex = n);
//}

//function showSlides(n) {
//    let i;
//    let slides = document.getElementsByClassName("mySlides");
//    let dots = document.getElementsByClassName("demo");
//    let captionText = document.getElementById("caption");
//    if (n > slides.length) { slideIndex = 1 }
//    if (n < 1) { slideIndex = slides.length }
//    for (i = 0; i < slides.length; i++) {
//        slides[i].style.display = "none";
//    }
//    for (i = 0; i < dots.length; i++) {
//        dots[i].className = dots[i].className.replace(" active", "");
//    }
//    slides[slideIndex - 1].style.display = "block";
//    dots[slideIndex - 1].className += " active";
//    captionText.innerHTML = dots[slideIndex - 1].alt;
//}