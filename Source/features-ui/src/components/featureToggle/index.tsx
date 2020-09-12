// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import { Feature, FeatureValueType } from '../../models';
import { Text, Toggle, PrimaryButton, DefaultButton } from '@fluentui/react';
import { useState } from "preact/hooks";

type Props = {
    feature: Feature,
    checked: boolean,
    handleFeatureChange: (feature: Feature, newValue: FeatureValueType) => void
};

const FeatureToggle: FunctionalComponent<Props> = (props) => {
    const {
        feature,
        checked,
        handleFeatureChange
    } = props;

    const {
        readonly,
        uiPrefix,
        uiSuffix
    } = feature;

    const [newValue, setNewValue] = useState(checked);

    const hasChanged = checked !== newValue;

    const canSave = hasChanged;
    const canCancel = hasChanged;

    const onToggle = () => {
        setNewValue(!newValue);
    }

    const onValidateButtonClicked = () => {
        handleFeatureChange(feature, newValue);
    }

    const onCancelButtonClicked = () => {
        setNewValue(checked);
    }

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
                checked={newValue}
                disabled={readonly}
                onChange={onToggle}
                prefix={uiPrefix || undefined}
                suffix={uiSuffix || undefined}
            />
            <p>
                {canSave && (
                    <PrimaryButton
                        text="Save"
                        onClick={() => onValidateButtonClicked()}
                        allowDisabledFocus
                    />
                )}
                {canCancel && (
                    <DefaultButton
                        text="Cancel"
                        onClick={() => onCancelButtonClicked()}
                        allowDisabledFocus
                        style={{ marginLeft: 12 }}
                    />
                )}
            </p>
        </div>
    );
};

export default FeatureToggle;
