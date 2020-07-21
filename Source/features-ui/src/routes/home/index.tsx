// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { useState, useEffect } from "preact/hooks";
import { Feature, FeatureType } from '../../models';
import { env } from '../../config';
import { Spinner, SpinnerSize } from '@fluentui/react';
import FeatureToggle from '../../components/featureToggle';
import FeatureTextInput from '../../components/featureTextInput';
import FeatureNumberInput from '../../components/featureNumberInput';
import FeatureCombobox from '../../components/featureCombobox';

const Home: FunctionalComponent = () => {
    const [loading, setLoading] = useState(false);
    const [features, setFeatures] = useState<Feature[]>([]);

    useEffect(() => {
        setLoading(true);

        fetch(env.apiEndpoint)
            .then<Feature[]>(res => res.json())
            .then(features => {
                setFeatures(features);
                setLoading(false);
            });
    }, []);

    const handleFeatureChange = (feature: Feature, newValue: FeatureType) => {
        const payload = {
            value: newValue
        };

        fetch(`${env.apiEndpoint}/${feature.name}/set`, {
            method: 'POST',
            body: JSON.stringify(payload)
        })
            .then<Feature>(res => res.json())
            .then(feature => {
                setFeatures(features => {
                    return features.map(f => {
                        if (f.name === feature.name) {
                            return feature;
                        }
                        return f;
                    });
                });
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

    return (
        <div class={style.home}>
            {features.map(f => {
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
