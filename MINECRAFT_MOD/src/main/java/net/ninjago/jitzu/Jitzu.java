package net.ninjago.jitzu;

import net.minecraft.world.phys.AABB;
import net.minecraft.world.phys.Vec3;
import net.minecraft.world.damagesource.DamageSource;
import net.minecraft.world.entity.Entity;
import net.minecraft.world.entity.LivingEntity;
import net.mcreator.ninjago.init.NinjagoModParticleTypes;
import net.minecraft.core.particles.ParticleTypes;
import net.minecraft.core.particles.SimpleParticleType;
import net.minecraft.server.level.ServerLevel;

public class Jitzu {

	private int spin;
	private int beam;

	public Jitzu() {
		spin = 0;
		beam = 0;
	}

	public void tick(Entity e) {
		if (e.getLevel() instanceof ServerLevel s) {
			if (spin > 0) {
				spin--;
				s.sendParticles((SimpleParticleType) (NinjagoModParticleTypes.SWEEP_ATTACK.get()), (e.getX()),
						(e.getY() + 0),
						(e.getZ()), 3, 0, 1, 0, 0);
			}
			if (beam > 0) {
				beam--;
				if (beam > 15)
					return;
				double x = (e.getX() + e.getLookAngle().x * (15 - beam));
				double y = (e.getY() + e.getEyeHeight());
				double z = (e.getZ() + e.getLookAngle().z * (15 - beam));

				s.sendParticles(
						ParticleTypes.SONIC_BOOM, x, y, z,
						1, 0, 0, 0, 0);

				AABB bb = new AABB(x, y, z, 0, 0, 0);
				bb = bb.inflate(0.5D);

				for (LivingEntity le : s.getEntitiesOfClass(LivingEntity.class, bb)) {
					if (le == e)
						continue;
					le.setRemainingFireTicks(20);
					le.hurt(DamageSource.sonicBoom(e), 10);
				}
			}
		}
	}

	public void spinGo(Entity e, int i) {
		if (!(e.getLevel() instanceof ServerLevel)) {
			e.setDeltaMovement(new Vec3(1.5 * (e.getLookAngle().x), 0.3, 1.5 * (e.getLookAngle().z)));
			spin = i;
		}
	}

	public void beamGo(Entity e) {
		if (!(e.getLevel() instanceof ServerLevel)) {
			e.setDeltaMovement(new Vec3(-1.5 * (e.getLookAngle().x), 0.5, -1.5 * (e.getLookAngle().z)));
			beam = 45;
		}
	}
}
