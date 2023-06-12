import { Persona } from "@fluentui/react-components"
import { useUser } from "../../state/user-state.ts"
import { User } from "../../models/user.ts"

type UserNotNullComponentProps = {
    user: User
}

const UserNotNullComponent = ({
    user: { name, avatarUrl, url },
}: UserNotNullComponentProps) => {
    const avatarComponent = {
        image: {
            src: avatarUrl,
        },
    }

    return (
        <div style={{ textAlign: "right" }}>
            <Persona name={name} secondaryText={url} avatar={avatarComponent} />
        </div>
    )
}

const UserComponent = () => {
    const user = useUser()

    if (user != null) {
        return <UserNotNullComponent user={user} />
    }

    return <></>
}

export default UserComponent
