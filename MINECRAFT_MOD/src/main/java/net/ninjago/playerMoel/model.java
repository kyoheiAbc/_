package net.ninjago.playerMoel;

import com.mojang.blaze3d.vertex.PoseStack;
import com.mojang.blaze3d.vertex.VertexConsumer;
import net.minecraft.client.model.HumanoidModel;
import net.minecraft.client.model.geom.ModelPart;
import net.minecraft.client.model.geom.PartPose;
import net.minecraft.client.model.geom.builders.CubeDeformation;
import net.minecraft.client.model.geom.builders.CubeListBuilder;
import net.minecraft.client.model.geom.builders.LayerDefinition;
import net.minecraft.client.model.geom.builders.MeshDefinition;
import net.minecraft.client.player.AbstractClientPlayer;

public class model extends HumanoidModel<AbstractClientPlayer> {

        public static LayerDefinition layerDefinition() {
                MeshDefinition meshdefinition = HumanoidModel.createMesh(CubeDeformation.NONE, 0.3F);

                meshdefinition.getRoot().addOrReplaceChild("cloak",
                                CubeListBuilder.create().texOffs(0, 0).addBox(-8F, 0F, 4F,
                                                16.0F, 24.0F, 2.0F),
                                PartPose.ZERO);

                return LayerDefinition.create(meshdefinition, 64, 64);
        }

        public final ModelPart cloak;

        public model(ModelPart p_170821_) {
                super(p_170821_);
                this.cloak = p_170821_.getChild("cloak");
        }

        @Override
        public void renderToBuffer(PoseStack poseStack, VertexConsumer vertexConsumer, int p_102036_, int p_102037_,
                        float red, float green, float blue, float alpha) {
                head.render(poseStack, vertexConsumer, p_102036_, p_102037_);
                leftLeg.render(poseStack, vertexConsumer, p_102036_, p_102037_);
                rightLeg.render(poseStack, vertexConsumer, p_102036_, p_102037_);
                leftArm.render(poseStack, vertexConsumer, p_102036_, p_102037_);
                rightArm.render(poseStack, vertexConsumer, p_102036_, p_102037_);
                body.render(poseStack, vertexConsumer, p_102036_, p_102037_);
                cloak.render(poseStack, vertexConsumer, p_102036_, p_102037_);
        }
}
