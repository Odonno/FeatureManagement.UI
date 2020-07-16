import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { useState, useEffect } from "preact/hooks";
import { Feature } from '../../models';
import { env } from '../../config';
import { Spinner, SpinnerSize , Text, Toggle } from '@fluentui/react';

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

    const handleFeatureChange = (feature: Feature) => {
        const payload = {
            value: !feature.enabled
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
                return (
                    <div>
                        <p>
                            <Text block variant="large">
                                {f.name}
                            </Text>
                            {f.description &&
                                <Text block variant="small">
                                    {f.description}
                                </Text>
                            }
                        </p>
                        <Toggle
                            checked={f.enabled}
                            onChange={() => handleFeatureChange(f)}
                        />
                    </div>
                );
            })}
        </div>
    );
};

export default Home;
