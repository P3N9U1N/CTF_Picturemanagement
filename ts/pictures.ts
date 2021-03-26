var pictureListContainer=document.querySelector(".pictureListContainer");
var pictureList=pictureListContainer.querySelector(".pictureList") as HTMLUListElement;
var img= pictureListContainer.querySelector(".pictureListColumn2>img") as HTMLImageElement;
pictureList.onclick= (ev:any)=>
{
  if (ev.target?.dataset?.imagepath)
  {
    img.src= "Download?fileName="+encodeURIComponent(ev.target.dataset.imagepath);
  }
};

