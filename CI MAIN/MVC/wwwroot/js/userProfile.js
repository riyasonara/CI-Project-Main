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
    debugger
    var selectedSkills = [];
    const skillsSelected = $('#s2 option');

    for (var i = 0; i < skillsSelected.length; i++) {
        selectedSkills.push(skillsSelected[i].value);
    }

    console.log(selectedSkills);
    $.ajax({
        url: '/User/SaveUserSkills',
        method:'POST',
        data: { selectedSkills: selectedSkills },

        success: function (response) {
            debugger;
            $('#userskilldiv').html($(response).find('#userskilldiv').html());
            window.location.href = '/user/userprofile';
            //document.getElementById('close').click();

        },
        error: function () {
            alert("could not comment");
        }
    });
});