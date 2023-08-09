import { useEffect, useState } from "react"

const initialRestaurantInfo = {
    name: "",
    type: "",
    foods:""
}

function Restuarant() {
    const [missionChoice, setMissionChice] = useState("")

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent:'center', gap:20 }}>
                <div>What do you want to do? </div>
                <div style={{ display:'flex', gap:20 }}>
                <button onClick={() => setMissionChice('add')}>Add a new restaurant</button>
                <button onClick={() => setMissionChice('lottery')}>Start restaurant lottery</button>
                    <button onClick={() => setMissionChice('see')}>See all restaurants</button>     
                </div>
            </div>
            <div style={{ marginTop:30, display: 'flex', justifyContent:'center' }}>
                {missionChoice === "add" && <RestaurantForm />}
                {missionChoice === "lottery" && <div>lottery</div>}
                {missionChoice === "see" && <RestaurantsTable />}
            </div>
        </>
    )
}

export default Restuarant

function RestaurantForm() {
    const [restaurantInfo, setRestaurantInfo] = useState(initialRestaurantInfo)

    function createRestaurant() {
        fetch('/Restaurant/CreateRestaurant', {
            method: 'POST',
            headers: { "content-type": "application/json" },
            body: JSON.stringify({
                restaurantName: restaurantInfo.name,
                restaurantType: restaurantInfo.type,
                restaurantStatus: "open",
                foods: restaurantInfo.foods.split(",").map(food => food.trim())
            })
        })
            .then((res) => res.json())
            .then((data) => {
                if (data.isSuccess) {
                    alert('created successfully')
                    setRestaurantInfo(initialRestaurantInfo)
                } else {
                    alert(data.message)
                }
            })
    }


    return (
        <div>
            <span> name:
                <input type="text" value={restaurantInfo.name} onChange={(e) => setRestaurantInfo(prev => ({ ...prev, name: e.target.value }))} />
            </span>
            <span> type:
                <input type="text" value={restaurantInfo.type} onChange={(e) => setRestaurantInfo(prev => ({ ...prev, type: e.target.value }))} />
            </span>
            <span> foods:
                <input type="text" value={restaurantInfo.foods} onChange={(e) => setRestaurantInfo(prev => ({ ...prev, foods: e.target.value }))} />
            </span>
            <button onClick={createRestaurant}>Create!</button>
        </div>
    )
}

function RestaurantsTable() {
    const [restaurants, setRestaurants] = useState([])
    console.log('restaurants', restaurants)

    useEffect(() => {
        async function fetchData() {
            try {
                const response = await fetch('/Restaurant/GetRestaurants');
                const data = await response.json();
                setRestaurants(data)
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        }
        fetchData();
    }, [])

    return (
        <table className="table table-striped table-hover" id="custom-table">
            <thead>
                <tr>
                    <th scope="col">
                       RestaurantName
                    </th>
                    <th scope="col">
                       RestaurantType
                    </th>
                    <th scope="col">
                        RestaurantStatus
                    </th>
                </tr>
            </thead>
            <tbody>
                {restaurants?.map((restaurant) => (
                    <tr key={restaurant?.restaurantUid}>
                        <td>{restaurant?.restaurantName}</td>
                        <td>{restaurant?.restaurantType}</td>
                        <td>{restaurant?.restaurantStatus}</td>
                    </tr>
                ))}
            </tbody>
        </table>
        )

}