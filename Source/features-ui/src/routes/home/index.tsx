// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { useState, useEffect } from "preact/hooks";
import { Feature, FeatureValueType, AuthenticationScheme } from '../../models';
import { env } from '../../config';
import { Spinner, SpinnerSize } from '@fluentui/react';
import FeatureToggle from '../../components/featureToggle';
import FeatureTextInput from '../../components/featureTextInput';
import FeatureNumberInput from '../../components/featureNumberInput';
import FeatureCombobox from '../../components/featureCombobox';
import { setAuthSchemes, useAuthSchemes, useFeatures, setFeatures, setSelectedAuthScheme, useSelectedAuthScheme } from "../../store";

const Home: FunctionalComponent = () => {
    const authSchemes = useAuthSchemes();
    const selectedAuthScheme = useSelectedAuthScheme();
    const features = useFeatures();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (authSchemes === undefined) {
            fetch(`${env.apiEndpoint}/auth/schemes`)
                .then<AuthenticationScheme[]>(res => res.json())
                .then(authSchemes => {
                    setAuthSchemes(authSchemes);
                    setLoading(false);
                });
        }
    }, [authSchemes]);

    useEffect(() => {
        if (authSchemes !== undefined && selectedAuthScheme === undefined) {
            const hasAnonymousAuth =
                authSchemes.length <= 0 || s.authSchemes.some(as => as.type === 'None');

            if (hasAnonymousAuth) {
                setSelectedAuthScheme({ type: 'None' });
            }
        }
    }, [authSchemes, selectedAuthScheme]);

    useEffect(() => {
        if (selectedAuthScheme !== undefined) {
            setLoading(true);

            fetch(env.apiEndpoint)
                .then<Feature[]>(res => res.json())
                .then(features => {
                    setFeatures(features);
                    setLoading(false);
                });
        }
    }, [selectedAuthScheme]);

    const handleFeatureChange = (feature: Feature, newValue: FeatureValueType) => {
        const payload = {
            value: newValue
        };

        fetch(`${env.apiEndpoint}/${feature.name}/set`, {
            method: 'POST',
            body: JSON.stringify(payload)
        })
            .then<Feature>(res => res.json())
            .then(feature => {
                const newFeatures = features.map(f => {
                    if (f.name === feature.name) {
                        return feature;
                    }
                    return f;
                });

                setFeatures(newFeatures);
            });
    };

    if (loading) {
        return (
            <div
                class={style.home}
                style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}
            >
                <Spinner
                    size={SpinnerSize.large}
                    label="The best features are yet to come..."
                />
            </div>
        );
    }

    if (selectedAuthScheme === undefined) {
        return (
            <div
                class={style.home}
                style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}
            >
                Please select an authentication scheme
            </div>
        );
    }

    return (
        <div class={style.home}>
            {features && features.map(f => {
                if (typeof f.value === 'boolean') {
                    const checked = f.value;

                    return <FeatureToggle
                        feature={f}
                        checked={checked}
                        handleFeatureChange={handleFeatureChange}
                    />;
                }

                if (f.choices) {
                    return <FeatureCombobox
                        feature={f}
                        value={f.value}
                        choices={f.choices}
                        handleFeatureChange={handleFeatureChange}
                    />;
                }

                if (typeof f.value === 'string') {
                    return <FeatureTextInput
                        feature={f}
                        value={f.value}
                        handleFeatureChange={handleFeatureChange}
                    />;
                }

                if (typeof f.value === 'number') {
                    return <FeatureNumberInput
                        feature={f}
                        value={f.value}
                        handleFeatureChange={handleFeatureChange}
                    />;
                }
            })}
        </div>
    );
};

export default Home;
