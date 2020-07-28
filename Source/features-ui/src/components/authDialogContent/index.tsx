import { FunctionalComponent, h } from "preact";
import { PrimaryButton } from '@fluentui/react';
import { AuthenticationScheme } from "../../models";
import { useState, useEffect } from "preact/hooks";
import { env } from "../../config";

type Props = {
    handleDialogDismissed: () => void;
};

const AuthDialogContent: FunctionalComponent<Props> = (props) => {
    const {
        handleDialogDismissed
    } = props;

    const [authSchemes, setAuthSchemes] = useState<AuthenticationScheme[]>([]);

    useEffect(() => {
        fetch(`${env.apiEndpoint}/auth/schemes`)
            .then<AuthenticationScheme[]>(res => res.json())
            .then(authSchemes => {
                setAuthSchemes(authSchemes);
            });
    }, []);

    return (
        <div>
            {authSchemes.map(s => {
                if (s.type === 'None') {
                    return (
                        <div>
                            No authentication
                        </div>
                    );
                }

                return (
                    <div>
                        <div>
                            Type: {s.type}
                        </div>
                        <div>
                            Key: {s.key}
                        </div>
                        <div>
                            Value: TODO
                        </div>
                    </div>
                );
            })}

            <PrimaryButton text="Ok" onClick={handleDialogDismissed} />
        </div>
    );
};

export default AuthDialogContent;
