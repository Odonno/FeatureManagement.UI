// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { Icon, Dialog, DialogType } from "@fluentui/react";
import { useState } from "preact/hooks";
import AuthDialogContent from '../../components/authDialogContent';

const modalProps = {
    isBlocking: false,
    styles: { main: { maxWidth: 450 } },
};

const dialogContentProps = {
    type: DialogType.largeHeader,
    title: 'Authentication',
    subText: 'Select the desired authentication scheme to use to obtain additional rights.',
};

const Header: FunctionalComponent = () => {
    const [hideDialog, setHideDialog] = useState(true);

    const handleAuthClicked = () => {
        setHideDialog(false);
    };

    const handleDialogDismissed = () => {
        setHideDialog(!hideDialog);
    };

    return (
        <>
            <header class={style.header}>
                <h1>Features</h1>
                <Icon
                    iconName="AuthenticatorApp"
                    onClick={handleAuthClicked}
                />
            </header>

            <Dialog
                hidden={hideDialog}
                onDismiss={handleDialogDismissed}
                dialogContentProps={dialogContentProps}
                modalProps={modalProps}
            >
                <AuthDialogContent handleDialogDismissed={handleDialogDismissed} />
            </Dialog>
        </>
    );
};

export default Header;
