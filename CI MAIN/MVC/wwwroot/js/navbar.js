$(document).ready(function () {
    filter();

});
function filter(sortValue,pg=1) {

    var Search = $("input[name='searchQuery']").val();
    //if (Search == '')
    //    Search = '';
    /*console.log(Search)*/


    var country = [];

    $("input[type='checkbox'][name='country']:checked").each(function () {
        country.push($(this).val());
    });
    //console.log(country)


    var city = [];
    $("input[type='checkbox'][name='city']:checked").each(function () {
        city.push($(this).val());
    });
    //console.log(city)

    var theme = [];
    $("input[type='checkbox'][name='theme']:checked").each(function () {
        theme.push($(this).val());
    });
    //console.log(theme)

    $.ajax({
        url: "/Employee/User/navbarfilters",
        type: "POST",
        data: { 'search': Search, 'pg': pg ,'sortValue': sortValue, 'country': country, 'city': city, 'theme': theme },

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
            //console.log("unchecked");
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

        //console.log(elementToBeRemoved);
        //console.log(current);
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



//==================================================== cascading =========================================

filteredCitites();
function filteredCitites() {
    //alert("hiiii");
    var missionCityDiv = document.getElementById("missioncity");
    var missionCountry = document.getElementById("missionCountry").value;
    var missioncity = document.getElementById("missioncity");

    //alert(missionCountry)
    while (missionCityDiv.hasChildNodes()) {
        missionCityDiv.removeChild(missionCityDiv.firstChild);
    }

    $.ajax({
        /* url: '@Url.Action("filterCity", "UserProfile")',*/
        url: '/Employee/UserProfile/filterCity',
        type: 'GET',
        data: { missionCountry },
        datatype: "json",
        success: function (result) {
            console.log(result)
            result.map((city, index) => {
                console.log(city)
                var newCityOption = document.createElement('option');
                newCityOption.value = city.cityId;
                newCityOption.innerText = city.name;
                console.log(newCityOption)
                missioncity.appendChild(newCityOption)
            })
        }
    });
}
