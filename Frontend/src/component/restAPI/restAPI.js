

function Url() {

    //На получение (отразятся данные на экране загрузки)
    fetch(`/reactions?article_id=489586`)//https://dev.to/reactions?article_id=489586
    .then(response => response.json())
    .then(data => {
        console.log(data) 
    });

    //Запрос на отправку данных 
    fetch('/article/fetch/post/user', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify({
            name: 'sasha',
            age: 123
        })
    }).catch((e) => {
        console.log(e.Url)
    })

  }
  
  export default Url;