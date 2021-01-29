const item_uri = 'api/Order/OrderItem';

// 取得product資訊
function getOrderItem() {
    let url = new URL(location.href);
    let itemId = url.searchParams.get('itemid');
    console.log(itemId);
    fetch(`${item_uri}?itemid=${itemId}`, { method: 'GET', })
        .then(response => response.json())
        .then(data => displayOrderItem(data))
        .catch(error => console.error('Unable to get order item.', error));
}

// 顯示資訊
function displayOrderItem(data)
{
    document.getElementById('prodName').textContent = data.name;
    document.getElementById('prodPrice').textContent = data.price;
    document.getElementById('prodCost').textContent = data.cost;
    document.getElementById('prodDesc').textContent = data.productDesc;

}

function addTextTd(tr, idx, str) {
    let td = tr.insertCell(idx)
    td.appendChild(document.createTextNode(str));
}