import { Button } from "@fluentui/react-components"
import { useNavigate } from "react-router-dom"

const GoToDashboard = () => {
    const navigate = useNavigate()

    const onGoToDashboard = () => {
        navigate("/")
    }

    return (
        <Button appearance="primary" onClick={onGoToDashboard}>
            Return to Dashboard
        </Button>
    )
}

export default GoToDashboard
