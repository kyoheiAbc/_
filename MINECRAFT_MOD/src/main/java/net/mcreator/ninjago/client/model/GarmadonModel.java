package net.mcreator.ninjago.client.model;

import com.mojang.blaze3d.vertex.PoseStack;
import com.mojang.blaze3d.vertex.VertexConsumer;
import net.mcreator.ninjago.entity.GarmadonEntity;
import net.minecraft.client.model.HumanoidModel;
import net.minecraft.client.model.geom.ModelPart;
import net.minecraft.client.model.geom.PartPose;
import net.minecraft.client.model.geom.builders.CubeDeformation;
import net.minecraft.client.model.geom.builders.CubeListBuilder;
import net.minecraft.client.model.geom.builders.LayerDefinition;
import net.minecraft.client.model.geom.builders.MeshDefinition;
import net.minecraft.client.model.geom.builders.PartDefinition;

public class GarmadonModel extends HumanoidModel<GarmadonEntity> {

	public static LayerDefinition LayerDefinitionCreate() {
		CubeDeformation cube = CubeDeformation.NONE;
		MeshDefinition meshdefinition = HumanoidModel.createMesh(cube, 0.3F);
		PartDefinition partdefinition = meshdefinition.getRoot();

		partdefinition.addOrReplaceChild("cloak",
				CubeListBuilder.create().texOffs(0, 0).addBox(-8F, 0F, 4F,
						16.0F, 24.0F, 2.0F),
				PartPose.ZERO);

		return LayerDefinition.create(meshdefinition, 64, 64);
	}

	public final ModelPart cloak;

	public GarmadonModel(ModelPart p_170821_) {
		super(p_170821_);
		this.cloak = p_170821_.getChild("cloak");
	}

	@Override
	public void renderToBuffer(PoseStack poseStack, VertexConsumer buffer, int packedLight, int packedOverlay,
			float red, float green, float blue, float alpha) {
		head.render(poseStack, buffer, packedLight, packedOverlay);
		leftLeg.render(poseStack, buffer, packedLight, packedOverlay);
		rightLeg.render(poseStack, buffer, packedLight, packedOverlay);
		leftArm.render(poseStack, buffer, packedLight, packedOverlay);
		rightArm.render(poseStack, buffer, packedLight, packedOverlay);
		body.render(poseStack, buffer, packedLight, packedOverlay);
		hat.render(poseStack, buffer, packedLight, packedOverlay);

		this.cloak.render(poseStack, buffer, packedLight, packedOverlay);
	}
}
