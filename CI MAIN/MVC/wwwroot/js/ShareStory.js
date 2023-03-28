const inputDiv = document.querySelector(".input-div")
const input = document.querySelector(".file")
const output = document.querySelector("output")
let imagesArray = []

input.addEventListener("change", () => {
    const files = input.files
    for (let i = 0; i < files.length; i++) {
        imagesArray.push(files[i])
    }
    displayImages()
})

inputDiv.addEventListener("drop", () => {
    e.preventDefault()
    const files = e.dataTransfer.files
    for (let i = 0; i < files.length; i++) {
        if (!files[i].type.match("image")) continue;

        if (imagesArray.every(image => image.name !== files[i].name))
            imagesArray.push(files[i])
    }
    displayImages()
})

function displayImages() {
    let images = ""
    imagesArray.forEach((image, index) => {
        images += `<div class="image">
                         <img src="${URL.createObjectURL(image)}" alt="image">
                         <span onclick="deleteImage(${index})" style="color:black;">&times;</span>
                       </div>`
    })
    output.innerHTML = images
}

function deleteImage(index) {
    imagesArray.splice(index, 1)
    displayImages()
}























//const multipleEvents = (element, eventNames, listener) => {
//    const events = eventNames.split(' ');

//    events.forEach(event => {
//        element.addEventListener(event, listener, false);
//    });
//};

//const fileUpload = () => {
//    const INPUT_FILE = document.querySelector('#upload-files');
//    const INPUT_CONTAINER = document.querySelector('#upload-container');
//    const FILES_LIST_CONTAINER = document.querySelector('#files-list-container');
//    const FILE_LIST = [];

//    multipleEvents(INPUT_FILE, 'click dragstart dragover', () => {
//        INPUT_CONTAINER.classList.add('active');
//    });

//    multipleEvents(INPUT_FILE, 'dragleave dragend drop change', () => {
//        INPUT_CONTAINER.classList.remove('active');
//    });

//    INPUT_FILE.addEventListener('change', () => {
//        const files = [...INPUT_FILE.files];

//        files.forEach(file => {
//            const fileURL = URL.createObjectURL(file);
//            const fileName = file.name;
//            const uploadedFiles = {
//                name: fileName,
//                url: fileURL
//            };


//            FILE_LIST.push(uploadedFiles);
//        });

//        FILES_LIST_CONTAINER.innerHTML = '';
//        FILE_LIST.forEach(addedFile => {
//            const content = `
//          <div class="form__files-container">
//            <span class="form__text">${addedFile.name}</span>
//            <div>
//              <a class="form__icon" href="${addedFile.url}" target="_blank" title="Preview">&#128065;</a>
//              <a class="form__icon" href="${addedFile.url}" title="Download" download>&#11123;</a>
//            </div>
//          </div>
//        `;

//            FILES_LIST_CONTAINER.insertAdjacentHTML('beforeEnd', content);
//        });
//    });
//};

//fileUpload();


