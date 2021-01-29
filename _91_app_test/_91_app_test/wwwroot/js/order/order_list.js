const uri = 'api/Order/Orders';
const ship_uri = 'api/Order/ShipOrders';
let account = 'UserA';

let orders = [];

// 取得訂單
function getOrders() {

    fetch(`${uri}?account=${account}`, {method: 'GET',})
    .then(response => response.json())
    .then(data => displayOrders(data))
    .catch(error => console.error('Unable to get orders.', error));
}

// 顯示訂單列表
function displayOrders(data) {
    const tBody = document.getElementById('orders');
    tBody.innerHTML = '';

    data.forEach(item => {

        // 加入checkbox
        let needShipCheckbox = document.createElement('input');
        needShipCheckbox.type = 'checkbox';
        needShipCheckbox.disabled = false;
        needShipCheckbox.checked = item.isComplete;
        if (item.status == 'To Be Shipped')
        {
            needShipCheckbox.disabled = true;
        }

        needShipCheckbox.name = 'changeStatus';
        needShipCheckbox.value = item.orderID;
        item.needShipCb = needShipCheckbox;
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(needShipCheckbox);

        addTextTd(tr, 1, item.orderID);

        // 加入product資訊連結
        let td = tr.insertCell(2)
        var a = document.createElement('a');
        var linkText = document.createTextNode(item.orderItem);
        a.appendChild(linkText);
        a.title = item.orderItem;
        a.href = `/Product.html?itemid=${item.productId}`;
        td.appendChild(a);

        // 其他訂單資訊
        addTextTd(tr, 3, item.price);
        addTextTd(tr, 4, item.cost);
        addTextTd(tr, 5, item.status);

    });

    orders = data;
}

// 更改訂單狀態
function shipOrder() {
    let needShipOrders = [];
    orders.forEach(item => {
        if (item.needShipCb.checked) {
            needShipOrders.push(item.orderID);
        }
    });

    postShipOrder(needShipOrders);
}

// 呼叫更改訂單狀態api
function postShipOrder(needShipOrders) {
    fetch(ship_uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'account': account
        },
        body: JSON.stringify(needShipOrders)
    }
    )
        .then(response => response.json())
        .then(data => showUpdateResult(`更新筆數${data.count}`))
        .catch(error => showUpdateResult('Unable to change orders.', error));
}

// 更新api回傳結果
function showUpdateResult(message) {
    window.alert(message);
    getOrders();
}

// 加入欄位資訊
function addTextTd(tr, idx, str)
{
    let td = tr.insertCell(idx)
    td.appendChild(document.createTextNode(str));
}