// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { PrimaryButton, DefaultButton, TextField } from '@fluentui/react';
import { useAuthSchemes, useSelectedAuthScheme, setSelectedAuthScheme } from "../../store";
import { AuthenticationScheme } from "../../models";
import { useState } from "preact/hooks";

type AuthCardSelectionProps = {
    authScheme: AuthenticationScheme;
    isSelected: boolean;
    handleAuthSchemeSelected: (authScheme: AuthenticationScheme) => void;
};

const AuthCardSelection: FunctionalComponent<AuthCardSelectionProps> = (props) => {
    const {
        authScheme,
        isSelected,
        handleAuthSchemeSelected
    } = props;

    const [value, setValue] = useState('');

    const onTextChanged = (e: React.KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const target = e.target as HTMLInputElement;
        setValue(target.value);
    }

    const onSelectClicked = () => {
        if (authScheme.type === 'None') {
            handleAuthSchemeSelected({ type: 'None', key: undefined, value: undefined });
        } else {
            handleAuthSchemeSelected({
                ...authScheme,
                value
            });
            setValue('');
        }
    }

    if (authScheme.type === 'None') {
        return (
            <div className={isSelected ? style.authCardSelected : style.authCard}>
                <div>
                    No authentication
                </div>
                {!isSelected &&
                    <PrimaryButton
                        style={{ marginTop: 8 }}
                        text="Select"
                        onClick={onSelectClicked}
                    />
                }
            </div>
        );
    }

    return (
        <div className={isSelected ? style.authCardSelected : style.authCard}>
            <div>
                Type: {authScheme.type}
            </div>
            <div 
                style={{ marginBottom: 8 }}
            >
                Key: {authScheme.key}
            </div>
            {isSelected ?
                <TextField
                    value="******"
                    readOnly={true}
                />
                :
                <TextField
                    placeholder="Value"
                    value={value}
                    onKeyUp={onTextChanged}
                />
            }
            {!isSelected &&
                <PrimaryButton
                    style={{ marginTop: 8 }}
                    text="Select"
                    onClick={onSelectClicked}
                />
            }
        </div>
    );
};

type Props = {
    handleDialogDismissed: () => void;
};

const AuthDialogContent: FunctionalComponent<Props> = (props) => {
    const {
        handleDialogDismissed
    } = props;

    const authSchemes = useAuthSchemes();
    const selectedAuthScheme = useSelectedAuthScheme();

    const handleAuthSchemeSelected = (authScheme: AuthenticationScheme) => {
        setSelectedAuthScheme(authScheme);
    };

    return (
        <div>
            {authSchemes.map(s => {
                const isSelected = s.type === selectedAuthScheme.type && s.key === selectedAuthScheme.key;

                return <AuthCardSelection
                    authScheme={s}
                    isSelected={isSelected}
                    handleAuthSchemeSelected={handleAuthSchemeSelected}
                />;
            })}

            <DefaultButton text="Ok" onClick={handleDialogDismissed} />
        </div>
    );
};

export default AuthDialogContent;
