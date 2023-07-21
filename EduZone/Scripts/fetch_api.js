async function getData(url = "", ) {
    const response = await fetch(url, {
        method: "GET", 
        mode: "cors", 
        credentials: "same-origin",
        cache: "no-cache",
        headers: {
        "Content-Type": "application/json",
        'X-CSRFToken':document.cookie.split('=')[1],
        },
        redirect: "follow",
    });
    return response.json();
}

