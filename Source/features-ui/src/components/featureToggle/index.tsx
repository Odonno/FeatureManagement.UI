// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import { Feature, FeatureType } from '../../models';
import { Text, Toggle } from '@fluentui/react';

type Props = {
    feature: Feature,
    checked: boolean,
    handleFeatureChange: (feature: Feature, newValue: FeatureType) => void
};

const FeatureToggle: FunctionalComponent<Props> = (props) => {
    const {
        feature,
        checked,
        handleFeatureChange
    } = props;

    return (
        <div>
            <p>
                <Text block variant="large">
                    {feature.name}
                </Text>
                {feature.description &&
                    <Text block variant="small">
                        {feature.description}
                    </Text>
                }
            </p>
            <Toggle
                checked={checked}
                onChange={() => handleFeatureChange(feature, !checked)}
            />
        </div>
    );
};

export default FeatureToggle;
