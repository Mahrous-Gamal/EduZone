let arrow = document.querySelectorAll(".arrow");
for (var i = 0; i < arrow.length; i++) {
    arrow[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;
        arrowParent.classList.toggle("showMenu");
    });
}
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");
sidebarBtn.addEventListener("click", () => {
    sidebar.classList.toggle("closee");
});
let numbers = document.querySelectorAll(".number-of-year");
let infoo = document.querySelectorAll(".year-info");
numbers.forEach((item) => {
    item.addEventListener("click", () => {
        infoo.forEach((ele) => {
            if (item.dataset.index == ele.dataset.index) {
                ele.classList.toggle("show");
            }
        });
    });
});

window.addEventListener(
    "keydown",
    function (e) {
        if (
            e.keyIdentifier == "U+000A" ||
            e.keyIdentifier == "Enter" ||
            e.keyCode == 13
        ) {
            if (e.target.nodeName == "INPUT" && e.target.type == "text") {
                e.preventDefault();

                return false;
            }
        }
    },
    true
);

window.onload = function () {
    /*
    const comm_btns = document.querySelectorAll(".comment-btn");
    if (comm_btns != null) {
      const get_timeline_parent = (elmnt) => {
        if (elmnt.parentElement.className === "timeline") {
          return elmnt.parentElement;
        }
        return get_timeline_parent(elmnt.parentElement);
      };
      comm_btns.forEach((element) => {
        element.addEventListener("click", (e) => {
          e.preventDefault();
          const btn = e.target;
          const comments_repos =
            get_timeline_parent(btn).querySelector(".comments_repos");
          comments_repos.classList.toggle("comment-repos-hide");
        });
      });
    }
    */
    const likes_span = document.querySelectorAll(".likes-span");
    if (likes_span != null) {
        const get_timeline_parent = (elmnt) => {
            if (elmnt.parentElement.className === "timeline") {
                return elmnt.parentElement;
            }
            return get_timeline_parent(elmnt.parentElement);
        };
        likes_span.forEach((element) => {
            element.addEventListener("mouseenter", (e) => {
                e.preventDefault();
                const btn = e.target;
                const likes_repos =
                    get_timeline_parent(btn).querySelector(".likes_repos");
                likes_repos.classList.remove("like-repos-hide");
                likes_repos.addEventListener("mouseleave", (event) => {
                    event.preventDefault();
                    likes_repos.classList.add("like-repos-hide");
                });
            });
        });
    }
    const up_img_btn = document.querySelector("#upload-img-btn");
    if (up_img_btn) {
        up_img_btn.addEventListener("click", (e) => {
            e.preventDefault();
            const img_inpt = document.querySelector("#file-input");
            img_inpt.click();
        });
    }
};