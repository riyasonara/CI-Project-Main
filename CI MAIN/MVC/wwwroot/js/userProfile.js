document.getElementById('imgdiv').addEventListener("click", e => {
    document.getElementById('imginput').click();
});


document.getElementById('imginput').addEventListener("change", e => {
    const reader = new FileReader(); // Create a new FileReader object
    reader.onload = function () {
        document.getElementById('user-profile-img').src = reader.result; // Set the source of the image tag to the selected image
    }
    reader.readAsDataURL(e.target.files[0]); // Read the selected file as a data URL
});


function ved1() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");

    for (var i = 0; i < a.length; i++) {
        if (a.options[i].selected == true) {
            a.options[i].selected = false
            c.add(a.options[i])

            ved1()
        }

    }
}
function ved2() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");

    for (var i = 0; i < c.length; i++) {
        if (c.options[i].selected == true) {
            c.options[i].selected = false
            a.add(c.options[i])
            ved2()
        }
    }
}
function ved3() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < a.length;) {
        c.add(a.options[c, i])
    }
}
function ved4() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < c.length;) {
        a.add(c.options[a, i])
    }
}


//document.getElementById('skillSave').onclick(function () { })
document.getElementById('skillSave').addEventListener("click", e => {

    var selectedSkills = [];
    const skillsSelected = $('#s2 option');

    for (var i = 0; i < skillsSelected.length; i++) {
        selectedSkills.push(skillsSelected[i].value);
    }

    console.log(selectedSkills);
    $.ajax({
        url: '/Employee/User/SaveUserSkills',
        method: 'POST',
        data: { selectedSkills: selectedSkills },

        success: function (response) {
            
            $('#userskilldiv').html($(response).find('#userskilldiv').html());
            window.location.href = '/Employee/user/userprofile';
            //document.getElementById('close').click();

        },
        error: function () {
            alert("could not comment");
        }
    });
});


function oldp() {
    $('#old-val').addClass('d-none');
}

function validatePassword(password) {
    // password must be at least 8 characters long
    if (password.length < 8) {
        return false;
    }

    // password must contain at least one uppercase letter
    if (!/[A-Z]/.test(password)) {
        return false;
    }

    // password must contain at least one lowercase letter
    if (!/[a-z]/.test(password)) {
        return false;
    }

    // password must contain at least one special character
    if (!/[\W_]/.test(password)) {
        return false;
    }

    // password is valid
    return true;
}

function ChangePassword() {
    var oldpass = document.getElementById('old').value;
    var newpass = document.getElementById('new').value;
    var confirmpass = document.getElementById('conf').value;

    const isValid = validatePassword(newpass)

    if (oldpass == "") {
        $('#old-val').removeClass('d-none');
        $('#old-val').addClass('d-inline!important');
    }

    else if (newpass == "") {
        Swal.fire('Please Enter New Password')
    }
    else if (confirmpass == "") {
        Swal.fire('Please Enter Confrim Password')
    }
    else if (newpass != confirmpass) {
        Swal.fire('Passowrd and Confrim Password Does not Match')

    }
    else if (isValid == false) {
        Swal.fire('Password is not Valid')
    }

    else {
        $.ajax({
            url: '/Employee/User/ChangePassword',
            type: 'POST',
            data: { old: oldpass, newp: newpass, conf: confirmpass },
            success: function (response) {
                if (response == true) {
                    alert("password change successfully");

                }
                else {
                    alert("Password Don't Get Change Due to Some Issues");
                }

            },
            error: function () {
                alert("could not comment");
            }
        });
    }
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
        url: '/Employee/User/filterCity',
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
