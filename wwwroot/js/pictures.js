"use strict";
var pictureListContainer = document.querySelector(".pictureListContainer");
var pictureList = pictureListContainer.querySelector(".pictureList");
var img = pictureListContainer.querySelector(".pictureListColumn2>img");
pictureList.onclick = (ev) => {
    if (ev.target?.dataset?.imagepath) {
        img.src = "Download?fileName=" + encodeURIComponent(ev.target.dataset.imagepath);
    }
};
//# sourceMappingURL=pictures.js.map