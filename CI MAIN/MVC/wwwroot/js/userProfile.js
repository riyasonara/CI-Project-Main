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