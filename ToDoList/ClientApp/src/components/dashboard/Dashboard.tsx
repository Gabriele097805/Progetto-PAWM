import { userLists } from "../../state/user-lists.ts"
import AddListButton from "./AddListButton.tsx"
import UserComponent from "../shared/UserComponent.tsx"
import { useRecoilValue } from "recoil"
import { useMemo } from "react"
import DashboardItem from "./DashboardItem.tsx"

const Dashboard = () => {
    const lists = useRecoilValue(userLists)

    const dashboard = useMemo(() => {
        return lists.map((i) => <DashboardItem key={i.id} item={i} />)
    }, [lists])

    return (
        <div>
            <UserComponent />
            <h1>Dashboard</h1>
            {dashboard}
            <AddListButton />
        </div>
    )
}

export default Dashboard
