import { Alert } from "@fluentui/react-components/unstable"
import { useRecoilValue } from "recoil"
import {
    appMessageSelector,
    useTooltipMessage,
} from "../../state/app-message-atom.ts"

const AppMessageViewer = () => {
    const { dismissTooltipMessage } = useTooltipMessage()
    const { show, intent, message } = useRecoilValue(appMessageSelector)

    const onDismiss = () => {
        dismissTooltipMessage()
    }

    if (show) {
        return (
            <div className="tooltip-container">
                <Alert
                    intent={intent ?? "info"}
                    className="tooltip-component"
                    action="Dismiss"
                    onClick={onDismiss}
                >
                    {message}
                </Alert>
            </div>
        )
    }

    return <></>
}

export default AppMessageViewer
